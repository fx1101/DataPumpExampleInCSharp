using System;
using System.Windows.Forms;
using DataPumpExampleInCSharp.Base;
using DataPumpExampleInCSharp.Helpers;

namespace DataPumpExampleInCSharp.Nst
{
    public class NstAPI : INstAPI
    {
        #region Events

        public event EventHandler NstClosing;
        public event RequestHistoricalBarsDelegate RequestedHistoricalBars;
        public event RemoveTickerDelegate TickerRemoved;
        public event UsernamePasswordChangedDelegate UsernamePasswordRecieved;
        public event BusyIsBusyChangedDelegate NstIsBusyChanged;

        #endregion

        #region Fields

        private readonly TextBox _nstTextBox = new TextBox();
        private bool _enableNstRequests = false;
        private bool _nstCurrentlyBusy = true;
        private string _username = string.Empty;
        private string _password = string.Empty;

        #endregion

        #region Properties

        // Nst Requests must be enabled before events are raised
        public bool EnableRequests
        {
            get
            {
                return _enableNstRequests;
            }
            set
            {
                _enableNstRequests = value;

                if (_enableNstRequests)
                {
                    NstFeed.SetDataFeedhwnd(_nstTextBox.Handle.ToInt32());
                }
                else
                {
                    NstFeed.SetDataFeedhwnd(0);
                }
            }
        }

        // Returns whether Nst is busy
        public bool NstIsBusy
        {
            get { return _nstCurrentlyBusy; }
            private set
            {
                if (value != _nstCurrentlyBusy && NstIsBusyChanged != null)
                    NstIsBusyChanged(value);
                _nstCurrentlyBusy = value;
            }
        }

        #endregion

        #region Constructor

        // User must specify if Username and Password are used when instantiating the NstAPI
        public NstAPI(bool requireUsernameAndPassword)
        {
            // If required, force NST to display the username and password of this user
            if (requireUsernameAndPassword)
                NstFeed.RequireUserIDPassword();

            // Listen for NST requests
            _nstTextBox.TextChanged += NstTextBox_TextChanged;
        }

        #endregion

        #region Event Handlers

        // Communication from NST occurs by changing the text in a TextBox using a Windows Handle
        void NstTextBox_TextChanged(object sender, EventArgs e)
        {
            ProcessNstTextBoxRequest(_nstTextBox.Text);
        }

        #endregion

        #region Public Methods

        // Request that NST return the user's Username and Password via raising and event
        public void GetUsernamePasswordIfNotBusy()
        {
            if (!_nstCurrentlyBusy)
            {
                NstFeed.GetUserIDPassword(ref _username, ref _password);
                if (UsernamePasswordRecieved != null)
                    UsernamePasswordRecieved(_username, _password);
            }
        }

        // Send a set of historical bars to NST
        public bool SendHistoricBars(string ticker, string name, string category, BarType barType, DateTime startDate, IBar[] bars)
        {
            float barInterval = -1 * BarTypeHelper.FromBarType(barType);

            int i = NstFeed.NSTSETUPDATA(
                         ticker,
                         name,
                         barInterval,
                         3,
                         category,
                         AODateTimeHelper.ToAODateTime(startDate));

            i += NstFeed.NSTSETLABEL(ticker, barInterval, 1, "Date/Time");
            i += NstFeed.NSTSETLABEL(ticker, barInterval, 5, "Close");
            i += NstFeed.NSTSETLABEL(ticker, barInterval, 6, "Volume");

            foreach (IBar bar in bars)
            {
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.Open);
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, 0);
                i += NstFeed.NSTCOMMITROW(ticker, barInterval);

                i += NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.High);
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, 0);
                i += NstFeed.NSTCOMMITROW(ticker, barInterval);

                i += NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.Low);
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, 0);
                i += NstFeed.NSTCOMMITROW(ticker, barInterval);

                i += NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.Close);
                i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, bar.Volume);
                i += NstFeed.NSTCOMMITROW(ticker, barInterval);
            }

            NstFeed.UpdatePercent(100);

            return i == -1 - 3 - 4 * bars.Length;
        }

        // Send a Tick to NST
        public bool SendTick(string ticker, BarType barType, DateTime startDate, float close, float volume)
        {
            float barInterval = -1 * BarTypeHelper.FromBarType(barType);

            int i = NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(startDate));
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, close);
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, volume);
            i += NstFeed.NSTCOMMITROW(ticker, barInterval);

            return i == -4;
        }

        // Send a Tick Bar to NST
        public bool SendBar(string ticker, string name, string category, BarType barType, IBar bar)
        {
            float barInterval = -1 * BarTypeHelper.FromBarType(barType);

            int i = NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.Open);
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, 0);
            i += NstFeed.NSTCOMMITROW(ticker, barInterval);

            i += NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.High);
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, 0);
            i += NstFeed.NSTCOMMITROW(ticker, barInterval);

            i += NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.Low);
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, 0);
            i += NstFeed.NSTCOMMITROW(ticker, barInterval);

            i += NstFeed.NSTSETVALUE(ticker, barInterval, 1, AODateTimeHelper.ToAODateTime(bar.StartDate));
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 2, bar.Close);
            i += NstFeed.NSTSETVALUE(ticker, barInterval, 3, bar.Volume);
            i += NstFeed.NSTCOMMITROW(ticker, barInterval);

            return i == - 4 * 4;
        }

        #endregion

        #region Private Methods

        // If a Nst has put a message in the TextBox then translate raise appropriate events.  Notice
        // that NST is busy until it gets what it wants
        private void ProcessNstTextBoxRequest(string message)
        {
            if (message.Length > 0)
            {
                NstIsBusy = true;

                if (message.CompareTo("*****UnloadEXE*****") == 0)
                {
                    // NST closing
                    EnableRequests = false;
                    if (NstClosing != null)
                        NstClosing(this, new EventArgs());
                }
                else if (message.CompareTo("*****UserIDPassword*****") == 0)
                {
                    // User has changed UserId and Password in NST
                    NstFeed.GetUserIDPassword(ref _username, ref _password);
                    if (UsernamePasswordRecieved != null)
                        UsernamePasswordRecieved(_username, _password);
                }
                else if (message.Substring(0, 22).CompareTo("*****RemoveTicker*****") == 0)
                {
                    // User has closed a chart and no longer requires tick data
                    string[] items = message.Split(new string[] { "!@#" }, StringSplitOptions.None);
                    string ticker = items[1];
                    BarType barType = BarTypeHelper.ToBarType(float.Parse(items[2]));
                    if (TickerRemoved != null)
                        TickerRemoved(ticker, barType);
                }
                else if (message.Split(new string[] { "!@#" }, StringSplitOptions.None).Length == 6)
                {
                    // User has opened a chart and requires history and tick data
                    string[] items = message.Split(new string[] { "!@#" }, StringSplitOptions.None);
                    string ticker = items[0];
                    string name = items[1];
                    string category = items[2];
                    BarType barType = BarTypeHelper.ToBarType(float.Parse(items[3]));
                    DateTime startDate = DateTime.Parse(items[4]);
                    string columnTemplate = items[5];
                    if (RequestedHistoricalBars != null)
                        RequestedHistoricalBars(ticker, name, category, barType, startDate, columnTemplate);
                }
                else
                {
                    throw new ApplicationException("Unknown NST Request:" + message);
                }

                NstIsBusy = false;
            }
        }

        #endregion
    }
}
