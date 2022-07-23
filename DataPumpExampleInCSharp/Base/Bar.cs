using System;

namespace DataPumpExampleInCSharp.Base
{
    public class Bar : IBar
    {
        public DateTime StartDate { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public float Volume { get; set; }
    }
}
