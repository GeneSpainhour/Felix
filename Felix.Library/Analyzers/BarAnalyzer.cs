using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;

namespace Felix.Library.Analyzers
{
    public class BarAnalyzerResult
    {
        public IBarDatum WorkingBar { get; set; }

        public bool IsValidTrendBar { get; set; }

        public Trend.TrendType Trend { get; set; }
    }
    public class BarAnalyzer
    {
        public static BarAnalyzerResult Analyze(IBarDatum workingBar, IBarDatum datum, Trend.TrendType trend)
        {
            BarAnalyzerResult result = new BarAnalyzerResult();

            if (workingBar==null && datum != null)
            {
                result.WorkingBar = datum;

                result.Trend = GetBarTrend(datum);

                result.IsValidTrendBar = true;
            }
            else
            {
                bool isValidHigh = datum.High > workingBar.High && datum.Low > workingBar.Low;

                bool isValidLow = datum.High < workingBar.High && datum.Low < workingBar.Low;

                result.IsValidTrendBar = isValidHigh || isValidLow;

                if (result.IsValidTrendBar)
                {
                    result.WorkingBar = datum;

                    result.Trend = isValidHigh ? Trend.TrendType.Up : isValidLow ? Trend.TrendType.Down : Trend.TrendType.Unknown;
                }
                else
                {
                    result.WorkingBar = workingBar;

                    result.Trend = trend;
                }
            }

            return result;
        }

        private static Trend.TrendType GetBarTrend (IBarDatum bar)
        {
            bool bUp = bar.Close > bar.Open;

            bool bDown = bar.Close < bar.Open;

            return bUp ? Trend.TrendType.Up : bDown ? Trend.TrendType.Down : Trend.TrendType.Unknown;
        }
    }
}
