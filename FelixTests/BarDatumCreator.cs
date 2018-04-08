using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Library.BLL;
using Felix.Data;

namespace FelixTests
{
    public static class BarDatumCreator
    {
        private static int MomentumPeriod = 9;

        private static List<MetaMapping> mappings = MarketDataMock.MetaMappings
            .Where(m => m.Property.Contains("A"))
            .ToList();


        public static IBarDatum CreateBar(IBar bar, List<IBarDatum> barEntries)
        {
            BarDatumBuilder bldr = new BarDatumBuilder(bar, mappings);

            bldr.Build(MomentumPeriod, barEntries);

            return bldr.Datum;
        }

        public static IEnumerable<IBarDatum> ToBarDatum(this IEnumerable<IBar> bars)
        {
            List<IBarDatum> barData = new List<IBarDatum>();

            bars.ToList().ForEach(b => barData.Add(CreateBar(b, barData)));

            return barData;
        }
    }

    
    
}
