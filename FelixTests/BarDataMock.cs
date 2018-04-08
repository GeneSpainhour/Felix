using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;
using Felix.MarketData.Repositories;
using Felix.Library;

namespace FelixTests
{
    public class BarDataMocks
    {
        public List<BarDatum> BarData { get; set; }

        public List<Bar> Bars { get; set; }

        public BarDataMocks()
        {
            Bars = BarDataDoc.Bars.ToList();

            BarData = new List<BarDatum>();
        }
    }

    public class BarData
    {
        static BarData()
        {
            AutoMapperConfig.Register();
        }

        private static IMarketRepository Repository = new MarketRepository();
        public static IEnumerable<IBar> Bars(string symbol, DateTime startDate, DateTime endDate)
        {
            var marketDataBars = Repository.Bars(symbol, startDate, endDate).ToList();

            var dataBars = AutoMapper.Mapper.Map<List<Felix.MarketData.Bar>, List<IBar>>(marketDataBars);

            return dataBars;
        }
    }


    public class BarDataDoc
    {
        private static int ContractId = 3;
        public static IEnumerable<Bar> Bars
        {
            get
            {
                List<Bar> bars = new List<Bar>();

                double baseHigh = 20.0;

                double baseLow = 10.0;

                for (int i = 1, c=61; i<c; ++i)
                {
                    int ndx = i;

                    int hr = i / 12;

                    int min = (i * 5) % 60;

                    Debug.WriteLine($"hr: {hr}, min:{min}");

                    DateTime t = new DateTime(2018, 2, 2, hr, min, 0);

                    double high = i * baseHigh;

                    double low = i * baseLow;

                    double open = ( high + low ) / (double)2;

                    double close = open;

                    int volume = i*100;

                    bars.Add(new Bar(ndx, ContractId, 5, t, high, low, open, close, volume));
                }

                return bars;
            }
        }

        public static IEnumerable<IBar> MockedBars
        {
            get
            {
                return new List<BarMock>()
                {
                    new BarMock{ContractId=1, Index=1, Period=5, Open=20.00,  High=40.00, Low=10.00, Close=10.00, Volume=10, Time=DateTime.Parse("2018-03-01") },
                    new BarMock{ContractId=1,  Index=2, Period=5, Open=15.00,  High=50.00, Low=20.00, Close=20.00, Volume=20 , Time=DateTime.Parse("2018-03-01") },
                    new BarMock{ContractId=1,  Index=3, Period=5, Open=25.00,  High=60.00, Low=30.00, Close=30.00, Volume=30, Time=DateTime.Parse("2018-03-01")  },
                    new BarMock{ContractId=1,  Index=4, Period=5, Open=30.00,  High=70.00, Low=40.00, Close=40.00, Volume=40 , Time=DateTime.Parse("2018-03-01") },
                    new BarMock{ContractId=1,  Index=5, Period=5, Open=35.00,  High=80.00, Low=50.00, Close=50.00, Volume=50, Time=DateTime.Parse("2018-03-01")  },
                    new BarMock{ContractId=1,  Index=6, Period=5, Open=40.00,  High=90.00, Low=60.00, Close=60.00, Volume=null , Time=DateTime.Parse("2018-03-01") }
                };
            }
        }

     
    }

    public class BarMock : IBar
    {
        public int BarId { get; set; }
        public int ContractId { get; set; }
        public int Period { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public int Index { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public DateTime Time { get; set; }
        public Nullable<int> Volume { get; set; }
    }
}
