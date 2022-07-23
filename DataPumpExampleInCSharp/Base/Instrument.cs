namespace DataPumpExampleInCSharp.Base
{
    public class Instrument
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public BarType BarType { get; set; }

        public Instrument(string ticker, string name, string category, BarType barType)
        {
            Ticker = ticker;
            Name = name;
            Category = category;
            BarType = barType;
        }
    }
}
