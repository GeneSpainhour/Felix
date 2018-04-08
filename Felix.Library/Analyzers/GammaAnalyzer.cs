using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;

namespace Felix.Library.Analyzers
{

    public class GammaAnalyzer
    {
        public static List<ProspectiveIR> GetValues(bool upTrend, IBarDatum data)
        {
            double baseValue = GetMultiplier(upTrend, data);

            double baseGamma = Math.Sqrt(baseValue);

            List<ProspectiveIR> values = new List<ProspectiveIR>();

            for (int i = 0; i < 11; ++i)
            {
                double deg = 30.00 + i * 15.00;

                double gd = deg / 180;

                double value = upTrend ? Math.Pow(baseGamma + gd, 2) : Math.Pow(baseGamma - gd, 2);

                values.Add(new ProspectiveIR(deg, value));
            }

            return values;
        }
        public static double GetMultiplier(bool uptrend, IBarDatum data)
        {
            double value = double.MinValue;

            switch (data.ContractId)
            {
                case 1: // actually the value we're looking for is in the marketId
                value = uptrend ? data.Low : data.High;
                break;
            }

            if (value == double.MinValue)
            {
                throw new Exception($"GammaAnalyzer Multiplier Error");

            }

            return value;
        }
    }
}
