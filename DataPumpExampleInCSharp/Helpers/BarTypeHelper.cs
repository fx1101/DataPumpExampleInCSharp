using System;
using DataPumpExampleInCSharp.Base;

namespace DataPumpExampleInCSharp.Helpers
{
    public static class BarTypeHelper
    {
        // Given a requested number of minutes return a BarType
        public static BarType ToBarType(float barInterval)
        {
            int bi = (int)Math.Round(barInterval, 0);
            if (bi >= 302400)
                return BarType.Monthly;
            else if (bi >= 10080)
                return BarType.Weekly;
            else if (bi >= 1440)
                return BarType.Daily;
            else if (bi >= 240)
                return BarType.Hourly4;
            else if (bi >= 60)
                return BarType.Hourly;
            else if (bi >= 15)
                return BarType.Minute15;
            else if (bi >= 5)
                return BarType.Minute5;
            else if (bi >= 1)
                return BarType.Minute;
            else
                return BarType.Tick;
        }

        // Return the number of minutes for a BarType
        public static float FromBarType(BarType barType)
        {
            switch (barType)
            {
                case BarType.Tick:
                    return 0;
                case BarType.Minute:
                    return 1;
                case BarType.Minute5:
                    return 5;
                case BarType.Minute15:
                    return 15;
                case BarType.Hourly:
                    return 60;
                case BarType.Hourly4:
                    return 240;
                case BarType.Daily:
                    return 1440;
                case BarType.Weekly:
                    return 10080;
                case BarType.Monthly:
                    return 302400;
                default:
                    return 0;
            }
        }

        // Return a timespan corresponding to the entered BarType
        public static TimeSpan ToTimeSpan(BarType barType)
        {
            switch (barType)
            {
                case BarType.Tick:
                    throw new ApplicationException("No TimeSpan for Tick");
                case BarType.Minute:
                    return new TimeSpan(0, 1, 0);
                case BarType.Minute5:
                    return new TimeSpan(0, 5, 0);
                case BarType.Minute15:
                    return new TimeSpan(0, 15, 0);
                case BarType.Hourly:
                    return new TimeSpan(1, 0, 0);
                case BarType.Hourly4:
                    return new TimeSpan(4, 0, 0);
                case BarType.Daily:
                    return new TimeSpan(1, 0, 0, 0);
                case BarType.Weekly:
                    return new TimeSpan(7, 0, 0, 0);
                case BarType.Monthly:
                    throw new ApplicationException("No TimeSpan for Month");
                default:
                    throw new ApplicationException("No TimeSpan for this BarType");
            }
        }
    }
}
