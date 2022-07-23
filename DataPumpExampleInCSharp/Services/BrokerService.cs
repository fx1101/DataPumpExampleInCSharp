using System;
using System.Threading;
using System.Collections.Generic;
using DataPumpExampleInCSharp.Base;
using DataPumpExampleInCSharp.Helpers;
using MtApi;

namespace DataPumpExampleInCSharp.Services
{
    // This BrokerService supplies random price data
    public static class BrokerService
    {
        private static readonly MtApiClient _apiClient = new MtApiClient();
        private static bool flagLoading = false;
        
        public static bool checkMktOpen()
        {
            DateTime dtLastBar = _apiClient.iTime("", ChartPeriod.PERIOD_M1, 0);
            DateTime dtSvrTime = _apiClient.TimeCurrent();
            TimeSpan lastBarMinusCurr = dtLastBar.Subtract(dtSvrTime);
            if (lastBarMinusCurr.Minutes >= 10)
                return false;
            return true;
        }

        public static bool isLoading()
        {
            return flagLoading;
        }

        public static void apiConnect()
        {
            _apiClient.BeginConnect(Properties.Settings.Default.MTAPIhost,
                                    Properties.Settings.Default.MTAPIport);
        }

        public static void apiDisconnect()
        {
            _apiClient.BeginDisconnect();
        }

        public static DateTime getLastDt(string ticker, BarType barType)
        {
            var period = xlatBarType(barType);
            return _apiClient.iTime(ticker, period, 0);
        }

        public static DateTime getLastDt(Instrument ticker)
        {
            return getLastDt(ticker.Ticker, ticker.BarType);
        }

        public static ChartPeriod xlatBarType(BarType barType)
        {
            if (barType == BarType.Minute)
                return ChartPeriod.PERIOD_M1;
            else if (barType == BarType.Minute5)
                return ChartPeriod.PERIOD_M5;
            else if (barType == BarType.Minute15)
                return ChartPeriod.PERIOD_M15;
            else if (barType == BarType.Hourly)
                return ChartPeriod.PERIOD_H1;
            else if (barType == BarType.Hourly4)
                return ChartPeriod.PERIOD_H4;
            else if (barType == BarType.Daily)
                return ChartPeriod.PERIOD_D1;
            else if (barType == BarType.Weekly)
                return ChartPeriod.PERIOD_W1;
            else if (barType == BarType.Monthly)
                return ChartPeriod.PERIOD_MN1;
            return ChartPeriod.PERIOD_M1;
        }

        public static IBar[] GetHistoricBars(DateTime lastFullBarDateTime, Instrument ticker)
        {
            return GetHistoricBars(lastFullBarDateTime, lastFullBarDateTime, ticker.BarType, ticker.Ticker);
        }

        // Return bars of MTAPI data
        public static IBar[] GetHistoricBars(DateTime startDate, DateTime endDate, BarType barType, string ticker)
        {
            int i = 0;
            DateTime dtTmp = startDate;
            var period = xlatBarType(barType);
            List<IBar> bars = new List<IBar>();
            DateTime dt = _apiClient.iTime(ticker, period, i);
            flagLoading = true;
            if (endDate < startDate)
                dtTmp = endDate;
            
            Nst.NstFeed.UpdatePercent(0);

            while (dt >= dtTmp)
            {
                IBar bar = GetHistoricBar(ticker, period, i);
                bars.Add(bar);
                dt = _apiClient.iTime(ticker, period, ++i);
            }

            if(bars.Count > 1)
                bars.Reverse();

            Nst.NstFeed.UpdatePercent(100);
            flagLoading = false;
            return bars.ToArray();
        }

        public static IBar GetHistoricBar(Instrument ticker, int Shift)
        {
            var period = xlatBarType(ticker.BarType);
            return GetHistoricBar(ticker.Ticker, period, Shift);
        }

        public static IBar GetHistoricBar(string ticker, ChartPeriod period, int Shift)
        {
            double open = _apiClient.iOpen(ticker,period,Shift);
            double close = _apiClient.iClose(ticker, period, Shift);
            double low = _apiClient.iLow(ticker, period, Shift);
            double high = _apiClient.iHigh(ticker, period, Shift);
            double volume = _apiClient.iVolume(ticker, period, Shift);

            return new Bar
            {
                StartDate = _apiClient.iTime(ticker, period, Shift),
                Open = (float)open,
                High = (float)high,
                Low = (float)low,
                Close = (float)close,
                Volume = (float)volume
            };
        }

        public static IBar GetHistoricBar(string ticker, ChartPeriod period, DateTime dt)
        {
            int i = 0;
            DateTime dtTmp = _apiClient.iTime(ticker, period, i);
            while (dtTmp > dt)
                dtTmp = _apiClient.iTime(ticker, period, ++i);
            return GetHistoricBar(ticker, period, i);
        }

        public static IBar GetHistoricBar(Instrument ticker, DateTime dt)
        {
            var period = xlatBarType(ticker.BarType);
            return GetHistoricBar(ticker.Ticker, period, dt);
        }

        // Return bars of randomly generated history from a given start date (max 50 bars)
        public static IBar[] GetHistoricBars(DateTime startDate, DateTime endDate, BarType barType)
        {
            // Calculate the number of bars to return
            List<IBar> bars = new List<IBar>();
            TimeSpan startMinusEndDate = endDate.Subtract(startDate);
            TimeSpan barTypeTimeSpan = BarTypeHelper.ToTimeSpan(barType);
            int barCount = (int)(startMinusEndDate.Ticks/barTypeTimeSpan.Ticks);
            barCount = barCount <= 50 ? barCount : 50;

            // Calculate the start date of first bar
            startMinusEndDate = new TimeSpan(barTypeTimeSpan.Ticks*barCount);
            startDate = endDate.Subtract(startMinusEndDate);

            // Add bars of history which are randomly generated
            Random random = new Random();
            for (int i = 0; i < barCount; i++)
            {
                IBar bar = GetHistoricBar(startDate, random);
                bars.Add(bar);
                startDate = startDate.Add(barTypeTimeSpan);
            }

            return bars.ToArray();
        }

        // Create a random bar starting at startDate
        public static IBar GetHistoricBar(DateTime startDate, Random random)
        {
            int open = random.Next(900, 1100);
            int close = random.Next(900, 1000);
            int low = random.Next(800, Math.Min(open, close));
            int high = random.Next(Math.Max(open, close), 1200);
            int volume = random.Next(1000000);

            return new Bar
            {
                StartDate = startDate,
                Open = open,
                High = high,
                Low = low,
                Close = close,
                Volume = volume
            };
        }

        // Create a random Tick starting at startDate
        public static IBar GetTick(DateTime startDate)
        {
            Random random = new Random();
            return GetTick(startDate, random);
        }

        // Create a random Tick starting at startDate
        public static IBar GetTick(DateTime startDate, Random random)
        {
            int close = random.Next(900, 1000);
            int volume = random.Next(1000000);

            return new Bar
            {
                StartDate = startDate,
                Close = close,
                Volume = volume
            };
        }
    }
}
