using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class BarSeriesObject : IBarSeriesObject
    {
        public int ContractId { get; set; }

        public DateTime Time { get; set; }

        public int Period { get; set; }

        public BarSeriesObject() { }

        public BarSeriesObject(int contractId, DateTime time, int period)
        {
            ContractId = contractId;

            Time = time;

            Period = period;
        }

    }
}
