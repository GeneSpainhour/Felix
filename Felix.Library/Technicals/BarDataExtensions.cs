using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Felix.Interfaces;
using Felix.Data;

namespace Felix.Library.Technicals
{
    public static class BarDataExtensions
    {
        private static List<PropertyInfo> Properties = typeof(IBarDatum).GetProperties().ToList();
        public static ValueDatum Sma(this IBar bar, IMetaMapping mapping,  IEnumerable<IBarDatum> data)
        {
            int period = mapping.Value.Value;

            double ave = double.MinValue;

            double slope = double.MinValue;

            double dSlope = double.MinValue;

            var previousBars = ToSet(data, period - 1);

            if (period - previousBars.Count <= 1)
            {

                ave = ( previousBars.Sum(b => b.Close) + bar.Close ) / (double)period;

                IBarDatum previousBar = previousBars[0];

                int minutes = ( bar.Time - previousBar.Time ).Minutes;

                double prevAve = (double)Properties.Where(p => p.Name == mapping.Property)
                    .First()
                    .GetValue(previousBar, null);

                prevAve = prevAve == double.MinValue ? 0 : prevAve;

                slope = ( ave - prevAve) / (double)minutes;

                double prevSlope = (double)Properties.Where(p => p.Name == $"D{mapping.Property}")
                        .First()
                        .GetValue(previousBar, null);

                prevSlope = prevSlope == double.MinValue ? 0 : prevSlope;

                dSlope = ( slope - prevSlope ) / (double)minutes;
            }

            ValueDatum newDatum = new ValueDatum(mapping.Property, ave, slope, dSlope);

            return newDatum;
        }

        public static ValueDatum Momentum(this IBar bar, int period, IEnumerable<IBarDatum> data)
        {
            double mom = double.MinValue;

            double slope = double.MinValue;

            double dSlope = double.MinValue;

            if (period - data.Count() <= 1)
            {
                var previousBars = ToSet(data, period - 1);

                IBarDatum periodBar = previousBars.Last();

                double prevClose = periodBar.Close == double.MinValue ? 0 : periodBar.Close;

                mom = bar.Close - prevClose;

                IBarDatum previousBar = previousBars.First();

                int minutes = ( bar.Time - previousBar.Time ).Minutes;

                double prevMomentum = previousBar.M == double.MinValue ? 0 : previousBar.M;

                slope = ( mom - prevMomentum ) / (double)minutes;

                double prevMomDelta = previousBar.DM == double.MinValue ? 0 : previousBar.DM;

                dSlope = ( slope - prevMomDelta ) / (double)minutes;
            }

            ValueDatum newDatum = new ValueDatum("M", mom, slope, dSlope);

            return newDatum;
        }

        private static List<IBarDatum> ToSet(IEnumerable<IBarDatum> data, int period)
        {
            List<IBarDatum> retList = new List<IBarDatum>();

            if (data.Count() >= period)
            {
                var inverted = data.OrderByDescending(t => t.Time).Take(period).ToList();

                for (int i = 0; i < period; ++i)
                {
                    retList.Add(inverted[i].Time == DateTime.MinValue ? new BarDatum() : inverted[i]);
                }

            }

            return retList;
        }
    }
}
