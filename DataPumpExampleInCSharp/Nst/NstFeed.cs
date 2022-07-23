using System.Runtime.InteropServices;

namespace DataPumpExampleInCSharp.Nst
{
    class NstFeed
    {
        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int NSTSETUPDATA(string ticker, string description, float barInterval,
            int numColumns, string category, double firstDateAvailable);

        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int NSTSETLABEL(string ticker, float barInterval, int col, string label);

        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int NSTSETVALUE(string ticker, float barInterval, int col, double val);

        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int NSTCOMMITROW(string ticker, float barInterval);

        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void SetDataFeedhwnd(int hWnd);

        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void UpdatePercent(int Percent);

        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void RequireUserIDPassword();

        [DllImport("NSTFeed.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void GetUserIDPassword(ref string UserID, ref string Password);

        // NOTE: The below is unused but a developer could implement if they wanted to

        /*[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetTimeZoneInformation(TIME_ZONE_INFORMATION lpTimeZoneInformation);

        [STAThread]
        public static void Main()
        {
            TIME_ZONE_INFORMATION lpTimeZoneInformation;
            lpTimeZoneInformation.DaylightDate.wMonth = 0;
            lpTimeZoneInformation.StandardDate.wMonth = 0;
            int x = GetTimeZoneInformation(lpTimeZoneInformation);
            double SubtractTime = 0.20833333333333334;
            TimeZoneBias = lpTimeZoneInformation.TimeZoneBias;
            TimeZoneBias = (TimeZoneBias / 1440.0) - SubtractTime;
            new DataPump().ShowDialog();
        }*/

        //// Nested Types
        //[StructLayout(LayoutKind.Sequential)]
        //private struct SYSTEMTIME
        //{
        //    public short wYear;
        //    public short wMonth;
        //    public short wDayOfWeek;
        //    public short wDay;
        //    public short wHour;
        //    public short wMinute;
        //    public short wSecond;
        //    public short wMilliseconds;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //private struct TIME_ZONE_INFORMATION
        //{
        //    public int TimeZoneBias;
        //    [VBFixedString(64), MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        //    public char[] StandardName;
        //    public DataPumpAPI.SYSTEMTIME StandardDate;
        //    public int StandardTimeZoneBias;
        //    [VBFixedString(64), MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        //    public char[] DaylightName;
        //    public DataPumpAPI.SYSTEMTIME DaylightDate;
        //    public int DaylightTimeZoneBias;
        //}
    }
}
