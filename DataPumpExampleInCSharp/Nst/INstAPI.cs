using System;
using DataPumpExampleInCSharp.Base;

namespace DataPumpExampleInCSharp.Nst
{
    public delegate void RequestHistoricalBarsDelegate(string ticker, string name, string category, BarType barType, DateTime startDate, string columnTemplate);
    public delegate void RemoveTickerDelegate(string ticker, BarType barType);
    public delegate void UsernamePasswordChangedDelegate(string username, string password);
    public delegate void BusyIsBusyChangedDelegate(bool busy);

    public interface INstAPI
    {
        event EventHandler NstClosing;
        event RequestHistoricalBarsDelegate RequestedHistoricalBars;
        event RemoveTickerDelegate TickerRemoved;
        event UsernamePasswordChangedDelegate UsernamePasswordRecieved;
        event BusyIsBusyChangedDelegate NstIsBusyChanged;

        bool EnableRequests { get; set; }
        bool NstIsBusy { get; }

        void GetUsernamePasswordIfNotBusy();
        bool SendHistoricBars(string ticker, string name, string category, BarType barType, DateTime startDate, IBar[] bars);
        bool SendTick(string ticker, BarType barType, DateTime startDate, float close, float volume);
        bool SendBar(string ticker, string name, string category, BarType barType, IBar bar);
    }
}
