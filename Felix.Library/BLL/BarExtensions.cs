using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Data;
using Felix.Interfaces;

namespace Felix.Library.BLL
{
    public static class BarDatumCreator
    {
        public static IBarDatum CreateBar (IBar bar, 
            IEnumerable<IMetaMapping> mappings, 
            int momentumPeriod, 
            List<IBarDatum> barEntries)
        {
            BarDatumBuilder bldr = new BarDatumBuilder(bar, mappings);

            bldr.Build(momentumPeriod, barEntries);

            return bldr.Datum;
        }

        public static IEnumerable<IBarDatum> ToBarDatum(this IEnumerable<IBar> bars, 
            int marketId, 
            int momentumPeriod)
        {
            BLL.IMetaMappings metaMappingObj = new BLL.MetaMappings();

            IEnumerable<IMetaMapping> mappings = metaMappingObj.AveMappings(marketId)
                .ToList();

            List<IBarDatum> data = new List<IBarDatum>();

            bars.ToList().ForEach(b => data.Add(CreateBar(b, mappings, momentumPeriod, data)));

            return data;
        }

      
    }
}
