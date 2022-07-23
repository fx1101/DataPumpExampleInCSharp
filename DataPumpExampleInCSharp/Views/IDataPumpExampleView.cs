using System;
using DataPumpExampleInCSharp.Base;

namespace DataPumpExampleInCSharp.Views
{
    // Limits the way that the Presenter can interact with the screen
    public interface IDataPumpExampleView
    {
        event EventHandler Load;
        event EventHandler UsernamePasswordRequested;
        event EventHandler SendTick;
        event EventHandler SendBar;

        string Username { get; set; }
        string Password { get; set; }
        bool Busy { get; set; }
        bool SendTickEnabled { get; set; }
        bool SendBarEnabled { get; set; }

        Instrument[] LiveTickers { get; }
        void AddTicker(Instrument instrument);
        void RemoveTicker(string ticker);

        void LogMessage(string message);
    }
}
