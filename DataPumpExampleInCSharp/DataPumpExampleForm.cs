using System;
using System.Windows.Forms;
using DataPumpExampleInCSharp.Presenters;
using DataPumpExampleInCSharp.Views;
using System.Collections.Generic;
using DataPumpExampleInCSharp.Base;

namespace DataPumpExampleInCSharp
{
    public partial class DataPumpExampleForm : Form, IDataPumpExampleView
    {
        #region Events, Fields and Constructors

        public event EventHandler UsernamePasswordRequested;
        public event EventHandler SendTick;
        public event EventHandler SendBar;

        bool _busy;
        private List<Instrument> _tickers = new List<Instrument>();

        public DataPumpExampleForm()
        {
            InitializeComponent();

            // This view instantiates a Presenter
            DataPumpExamplePresenter _presenter = new DataPumpExamplePresenter(this);
        }

        #endregion

        public string Username
        {
            get { return usernameTextBox.Text; }
            set { usernameTextBox.Text = value; }
        }

        public string Password 
        {
            get { return passwordTextBox.Text; }
            set { passwordTextBox.Text = value; } 
        }

        // Set NST Busy
        public bool Busy
        {
            get
            {
                return _busy;
            }
            set
            {
                _busy = value;
                getUsernamePasswordButton.Enabled = !value;
                busyTextBox.Text = _busy ? "NST Busy... Please wait." : "";
            }
        }

        // Disable send tick button
        public bool SendTickEnabled
        {
            get { return sendTickButton.Enabled; }
            set { sendTickButton.Enabled = value; }
        }

        // Disable send bar button
        public bool SendBarEnabled
        {
            get { return sendBarButton.Enabled; }
            set { sendBarButton.Enabled = value; }
        }

        #region Ticker Functionality

        // Get a list of Live Tickers
        public Instrument[] LiveTickers
        {
            get { return _tickers.ToArray(); }
        }

        // Add tickers to the ListBox
        public void AddTicker(Instrument ticker)
        {
            if (!_tickers.Contains(ticker))
                _tickers.Add(ticker);
            tickersListBox.Items.Clear();
            foreach (Instrument instrument in _tickers)
            {
                tickersListBox.Items.Add(instrument.Ticker);
            }
        }

        // Remove a Ticker from the ListBox
        public void RemoveTicker(string ticker)
        {
            // Remove any instruments with this ticker
            List<Instrument> newInstruments = new List<Instrument>();
            foreach (Instrument instrument in _tickers)
            {
                if (instrument.Ticker != ticker)
                    newInstruments.Add(instrument);
            }
            _tickers = newInstruments;

            // Update ListBox
            tickersListBox.Items.Clear();
            foreach (Instrument instrument in _tickers)
            {
                tickersListBox.Items.Add(instrument.Ticker);
            }
        }

        #endregion

        // Add a log message to the ListBox
        public void LogMessage(string message)
        {
            logListBox.Items.Add(message);
        }

        #region Button Click Event Handlers

        private void getUsernamePasswordButton_Click(object sender, EventArgs e)
        {
            if (UsernamePasswordRequested != null)
                UsernamePasswordRequested(sender, e);
        }

        private void sendTickButton_Click(object sender, EventArgs e)
        {
            if (SendTick != null)
                SendTick(sender, e);
        }

        private void sendBarButton_Click(object sender, EventArgs e)
        {
            if (SendBar != null)
                SendBar(sender, e);
        }

        #endregion

        private void DataPumpExampleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
