using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Library;
using Felix.Data;

namespace FelixTests
{
    public class BarMocks
    {
        static BarMocks()
        {
            AutoMapperConfig.Register();
        }

        public static int Year = 2018;

        public static int Month = 3;

        public static int Day = 11;

        public static int Hour = 8;

        private static int ContractId = 1;

        private static int Volume = 100;

        public static IEnumerable<IBar> Uptrend
        {
            get
            {
                return UpTrend.Bars(Year, Month, Day, Hour);
            }
        }

        public static List<IBar> UpMove(int barCount, double initialLow)
        {
            List<IBar> bars = new List<IBar>();

            for (int i = 0; i< barCount; ++i)
            {
                int ndx = bars.Count + 1;

                int rawHour = Hour + i / 12;

                int hour =rawHour % 24;

                int day = Day + rawHour / 24;

                double low = initialLow + (double)(i * 20);

                double high = low + 50;

                DateTime time = new DateTime(Year, Month, day, hour, i % 12, 0);

                double open = low + 10;

                double close = high - 10;

                IBar bar = new Bar(ndx, ContractId, 5, time, high, low, open, close, Volume);

                bar.BarId = ndx;

                bars.Add(bar);
            }

            return bars;
        }

        public static List<IBar> DownMove (int barCount, double initialHigh)
        {
            List<IBar> bars = new List<IBar>();

            for (int i = 0; i < barCount; ++i)
            {
                int ndx = bars.Count + 1;

                int hour = Hour + i / 12;

                double high = initialHigh - (double)( i * 20 );

                double low = high - 50;

                DateTime time = new DateTime(Year, Month, Day, hour, i % 12, 0);

                double close = low + 10;

                double open = high - 10;

                IBar bar = new Bar(ndx, ContractId, 5, time, high, low, open, close, Volume);

                bar.BarId = ndx;

                bars.Add(bar);
            }

            return bars;
        }
    }

    public class Trend
    {
        private static int ContractId = 1;

        private static int Volume = 100;

        public List<double> Opens { get; set; }
        public List<double> Highs { get; set; }
        public List<double> Lows { get; set; }
        public List<double> Closes { get; set; }

        public Trend(
            List<double> opens,
            List<double> highs,
            List<double> lows,
            List<double> closes
            )
        {
            Opens = opens;
            Highs = highs;
            Lows = lows;
            Closes = closes;
        }

        public IEnumerable<IBar> Bars(int year, int month, int day, int hour, int period = 5)
        {
            List<IBar> bars = new List<IBar>();

            for (int i = 0, c = Opens.Count; i < c; ++i)
            {
                int ndx = bars.Count + 1;

                var bar = new Bar(ndx, ContractId, period, new DateTime(year, month, day, hour, i, 0), Highs[i], Lows[i], Opens[i], Closes[i], Volume);

                bar.BarId = ndx;

                bars.Add(bar);
            }

            return bars;
        }
    }
}
