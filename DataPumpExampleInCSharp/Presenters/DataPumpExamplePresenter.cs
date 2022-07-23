using System;
using System.Collections.Generic;
using DataPumpExampleInCSharp.Helpers;
using DataPumpExampleInCSharp.Views;
using DataPumpExampleInCSharp.Nst;
using System.Windows.Forms;
using DataPumpExampleInCSharp.Base;
using DataPumpExampleInCSharp.Services;

namespace DataPumpExampleInCSharp.Presenters
{
    public class DataPumpExamplePresenter
    {
        #region Fields

        private IDataPumpExampleView _view;
        private INstAPI _nst;
        private DateTime _endDate;
        DateTime _lastFullBarDateTime = DateTime.MinValue;
        Timer timerMTAPI_NS;

        #endregion

        #region Constructor

        // Instantiate a presenter for a given view
        public DataPumpExamplePresenter(IDataPumpExampleView view)
        {
            _view = view;
            _view.Load += View_Load;
            _view.UsernamePasswordRequested += View_UsernamePasswordRequested;
            _view.SendTick += View_SendTick;
            _view.SendBar += View_SendBar;
        }

        #endregion

        #region View Event Handlers

        void View_Load(object sender, EventArgs e)
        {
            // Show message that NST is busy
            _view.Busy = true;

            // Instantiate the NstAPI(require username and password)
            _nst = new NstAPI(true);
            _nst.RequestedHistoricalBars += Nst_RequestedHistoricalBars;
            _nst.UsernamePasswordRecieved += Nst_UsernamePasswordSet;
            _nst.NstIsBusyChanged += Nst_BusyChanged;
            _nst.TickerRemoved += Nst_TickerRemoved;
            _nst.NstClosing += Nst_NstClosing;
            _nst.EnableRequests = true;
            timerMTAPI_NS = new Timer();
            timerMTAPI_NS.Interval = 15000;
            timerMTAPI_NS.Tick += TimerMTAPI_NS_Tick;
            timerMTAPI_NS.Start();
            BrokerService.apiConnect();
        }

        private void TimerMTAPI_NS_Tick(object sender, EventArgs e)
        {
            SendBar();
            //throw new NotImplementedException();
        }

        void SendBar()
        {
            if (_view.LiveTickers != null && !_nst.NstIsBusy && BrokerService.checkMktOpen())
            {
                // Send a bar for each ticker
                foreach (Instrument ticker in _view.LiveTickers)
                {
                    IBar[] bars = BrokerService.GetHistoricBars(_lastFullBarDateTime, ticker);
                    _nst.SendHistoricBars(ticker.Ticker, ticker.Name, ticker.Category, ticker.BarType, _lastFullBarDateTime, bars);
                    _lastFullBarDateTime = BrokerService.getLastDt(ticker);
                    _view.LogMessage(string.Format("Bar sent for {0}", ticker.Ticker));
                }
            }
        }

        // The user has requested a username and password so tell NST
        // to send it to us by raising the UsernamePasswordRecieved event
        void View_UsernamePasswordRequested(object sender, EventArgs e)
        {
            _view.LogMessage("Username and Password Requested");
            _nst.GetUsernamePasswordIfNotBusy();
        }

        // The user has requested we send a tick for each live ticker
        void View_SendTick(object sender, EventArgs e)
        {
            _view.SendBarEnabled = false;

            if (_view.LiveTickers != null && !_nst.NstIsBusy && BrokerService.isLoading())
            {
                foreach (Instrument ticker in _view.LiveTickers)
                {
                    // Get a single random Bar
                    IBar bar = BrokerService.GetTick(DateTime.Now);
                    
                    // Send the new Tick to NST
                    _nst.SendTick(ticker.Ticker, ticker.BarType, bar.StartDate, bar.Close, bar.Volume);
                    _view.LogMessage(string.Format("Tick sent for {0}", ticker.Ticker));
                }
            }
        }

        // The user has requested we send a bar for each live ticker
        void View_SendBar(object sender, EventArgs e)
        {
            _view.SendTickEnabled = false;

            if (_view.LiveTickers != null && !_nst.NstIsBusy && !BrokerService.isLoading())
            {
                // Get the date for the next bar
                if (_view.LiveTickers.Length > 0)
                {
                    // Get the next DateTime for which we will generate a Bar
                    Instrument ticker = _view.LiveTickers[0];
                    TimeSpan barLength = BarTypeHelper.ToTimeSpan(ticker.BarType);
                    _lastFullBarDateTime = _lastFullBarDateTime.Add(barLength);
                }

                // Send a bar for each ticker
                foreach (Instrument ticker in _view.LiveTickers)
                {
                    // Get a single random Bar
                    //IBar bar = BrokerService.GetHistoricBar(_lastFullBarDateTime);
                    IBar bar = BrokerService.GetHistoricBar(ticker, _lastFullBarDateTime);

                    // Send the new Bar to NST
                    IBar[] bars = new IBar[] { bar };
                    _nst.SendBar(ticker.Ticker, ticker.Name, ticker.Category, ticker.BarType, bar);
                    _view.LogMessage(string.Format("Bar sent for {0}", ticker.Ticker));
                }
            }
        }

        #endregion

        #region NST Event Handlers

        // Indicate that Nst is busy
        void Nst_BusyChanged(bool busy)
        {
            _view.Busy = busy;
        }

        // NST has requested historical bars
        void Nst_RequestedHistoricalBars(string ticker, string name, string category, BarType barType, DateTime startDate, string columnTemplate)
        {
            IBar[] bars;
            bool isAleadyIns = false;
            _view.LogMessage(string.Format("Loading data for {0} ticker", ticker));

            // Get a new start date for data 
            startDate = GetMidnight(startDate);
            _endDate = GetMidnight(DateTime.Now);
            _view.LogMessage(string.Format("Start Date = {0}", startDate));
            _view.LogMessage(string.Format("End Date = {0}", _endDate));
            string[] names = name.Split(':');
            string namegrp = names[0].TrimEnd();
            
            // Get bars of historic data from broker
            if(namegrp.CompareTo("MT4") == 0)
                bars = BrokerService.GetHistoricBars(startDate,_endDate,barType,ticker);
            else
                bars = BrokerService.GetHistoricBars(startDate, _endDate, barType);
            _view.LogMessage(string.Format("Loaded {0} bars", bars.Length));

            // Store the DateTime of the last bar as a basis for sending the next bars and tick
            if (namegrp.CompareTo("MT4") == 0)
                _lastFullBarDateTime = BrokerService.getLastDt(ticker, barType);
            else
            {
                if (bars.Length > 0)
                    _lastFullBarDateTime = bars[0].StartDate;
            }

            // Send historic bars to Nst
            if (bars.Length > 0)
                _nst.SendHistoricBars(ticker, name, category, barType, startDate, bars);
            _view.LogMessage(string.Format("Sending {0} bars", bars.Length));

            // Add a ticker to monitor this instrument
            Instrument instrument = new Instrument(ticker, name, category, barType);
            foreach (Instrument intrumentListItem in _view.LiveTickers)
            {
                if (intrumentListItem == instrument)
                    isAleadyIns = true;
            }
            if(!isAleadyIns)
                _view.AddTicker(instrument);
            _view.LogMessage(string.Format("Ticker {0} added", instrument.Ticker));
        }

        // Update the username and password on the screen if they have been changed in NST
        void Nst_UsernamePasswordSet(string username, string password)
        {
            _view.Username = username;
            _view.Password = password;
            _view.LogMessage("Username and Password retrieved");
            _view.LogMessage(string.Format("Username is {0}", username));
            _view.LogMessage(string.Format("Password is {0}", password));
        }

        // A chart has been closed so don't follow the ticks from this instrument
        void Nst_TickerRemoved(string ticker, BarType barType)
        {
            // Remove a ticker to monitor this instrument
            _view.RemoveTicker(ticker);
            _view.LogMessage(string.Format("Ticker {0} removed", ticker));
        }

        // Close this application when NST closes
        void Nst_NstClosing(object sender, EventArgs e)
        {
            _view.LogMessage("NST Closing");
            BrokerService.apiDisconnect();
            Application.Exit();
        }

        #endregion

        #region Private Methods

        // Return a DateTime for midnight of a specfied day
        private static DateTime GetMidnight(DateTime dateTime)
        {
            DateTime midnight = dateTime.Date;
            return midnight;
        }

        #endregion
    }
}
