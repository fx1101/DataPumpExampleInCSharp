using System;

namespace DataPumpExampleInCSharp.Helpers
{
    public static class AODateTimeHelper
    {
        // Convert a double to a DateTime
        public static DateTime FromAODateTime(double aoDateTime)
        {
            return DateTime.FromOADate(aoDateTime);
        }

        // Convert a DateTime to a double
        public static double ToAODateTime(DateTime dateTime)
        {
            return dateTime.ToOADate();
        }
    }
}
