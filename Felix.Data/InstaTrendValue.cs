using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class InstaTrendValue : BarSeriesObject, IInstaTrendValue
    {
        public double Value { get; set; }

        public double DValue { get; set; }

        public double D2Value { get; set; }

        public InstaTrendValue(): base() { }

        public InstaTrendValue(int contractId, 
            DateTime time, 
            int period, 
            double value=0, 
            double dValue = 0,
            double d2Value = 0) 
            : base(contractId, time, period)
        {
            Value = value;

            DValue = dValue;

            D2Value = d2Value;
        }
    }
}
