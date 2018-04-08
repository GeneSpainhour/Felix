using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class ValueDatum : IValueDatum
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double Slope { get; set; }

        public double DSlope { get; set; }
        public ValueDatum() { }

        public ValueDatum(string name, double value=double.MinValue, double slope = double.MinValue, double dSlope= double.MinValue)
        {
            Name = name;

            Value = value;

            Slope = slope;

            DSlope = dSlope;
        }
    }
}
