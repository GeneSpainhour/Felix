using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class Bar : IBar
    {
        public int BarId { get; set; }

        public int ContractId { get; set; }

        public int Period { get; set; }
        public int Index { get; set; }
        public DateTime Time { get; set; }

        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public Nullable<int> Volume { get; set; }

        public Bar () { }

        public Bar (int index, int contractId, int period, DateTime time, double high, double low, double open, double close, int volume)
        {
            Index = index;

            Time = time;

            High = high;

            Low = low;

            Open = open;

            Close = close;

            Volume = volume;

            ContractId = contractId;

            Period = period;
        }
    }
}
