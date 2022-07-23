using System;

namespace DataPumpExampleInCSharp.Base
{
    public interface IBar
    {
        DateTime StartDate { get; }
        float Open { get; }
        float High { get; }
        float Low { get; }
        float Close { get; }
        float Volume { get; }
    }
}
