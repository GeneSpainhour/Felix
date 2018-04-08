using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Felix.Data;
using Felix.Interfaces;

namespace Felix.Library.Technicals.Indicators
{
    public static class Indicators
    {
        public static IInstaTrendValue InstaTrend(this IBar bar, IEnumerable<IBar> bars, double alpha, IEnumerable<IInstaTrendValue> trend)
        {
            List<IInstaTrendValue> Trend = trend
                .Where ( t => t.Time < bar.Time)
                .OrderByDescending (t => t.Time)
                .Take(2)
                .ToList();

            List<double> Prices = bars
                .Where ( b => b.Time < bar.Time)
                .OrderByDescending(b => b.Time)
                .Take(3)
                .Select(b => b.Close)
                .ToList();

            double price = bar.Close;

            double alphaSquared = Math.Pow(alpha, 2);

            InstaTrendValue value = new InstaTrendValue(bar.ContractId, bar.Time, bar.Period);
  
            if (Prices.Count < 3)
            {
                value.Value = price;

            }
            else
            {
                value.Value = ( alpha - 0.25 * alphaSquared ) * Prices[0] + .5 * alphaSquared * Prices[1] - ( alpha - .75 * alphaSquared )*Prices[2]
                    + 2 * ( 1 - alpha ) * Trend[0].Value - Math.Pow(( 1 - alpha ), 2) * Trend[1].Value;
            }

            if (Trend.Count > 0 )
            {
                double totalMinutes = ( Trend[0].Time - value.Time ).TotalMinutes;

                value.DValue = ( Trend[0].Value - value.Value ) /totalMinutes;

                value.D2Value = ( Trend[0].DValue - value.DValue ) / totalMinutes;
            }

            return value;

        }

        private static void ExaminePrices (List<double> prices)
        {
            if (prices.Count < 7)
            {
                return;
            }

            int i = 0;

            prices.ForEach(p => Debug.WriteLine($"{i}: {prices[i++]}"));
        }

        private static void ExamineTrend(List<IInstaTrendValue> trend)
        {
            if (trend.Count < 7)
            {
                return;
            }

            List<PropertyInfo> properties = typeof(IInstaTrendValue).GetProperties().ToList();

            StringBuilder bldr = new StringBuilder();

            for (int i = 0; i< trend.Count; ++i)
            {
                var item = trend[i];

                bldr.Append($"{i}: ");

                properties.ForEach(p => 
                {
                    bldr.Append($"{p.Name}: {p.GetValue(item, null)}, ");
                });

                Debug.WriteLine(bldr.ToString());

                bldr.Clear();
            }
        }

        public static IEnumerable<IInstaTrendValue> InstaTrend(this IEnumerable<IBar> bars, double alpha)
        {
            List<IInstaTrendValue> values = new List<IInstaTrendValue>();

            foreach( IBar bar in bars)
            {
                var value = bar.InstaTrend(bars, alpha, values);

                values.Add(value);
            }

            return values;
        }
    }
}
