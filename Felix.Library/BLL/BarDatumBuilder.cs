using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Data;
using Felix.Library.Technicals;

namespace Felix.Library.BLL
{

    public class BarDatumBuilder
    {
        public IBarDatum Datum { get; set; }

        public Felix.Interfaces.IBar Bar { get; set; }

        public List<IMetaMapping> Mappings { get; set; }

        private static List<PropertyInfo> properties = typeof(IBarDatum).GetProperties().ToList();

        public BarDatumBuilder (Felix.Interfaces.IBar bar, IEnumerable<IMetaMapping> mappings)
        {
            Datum = new BarDatum(bar);

            Mappings = mappings.ToList();

            Bar = bar;
        }

        public void Build (int momentumPeriod, IEnumerable<IBarDatum> barData)
        {
            int period = momentumPeriod;

            BuildMomentum(period, barData);

            foreach (var mapping in Mappings)
            {
                BuildMeta(mapping, barData);
            }
        }

        private void BuildMomentum (int period, IEnumerable<IBarDatum> barData)
        {
            ValueDatum mom = Bar.Momentum(period, barData);

            Datum.M = mom.Value;

            Datum.DM = mom.Slope;

            Datum.D2M = mom.DSlope;
        }

        private void BuildMeta(IMetaMapping mapping, IEnumerable<IBarDatum> barData)
        {
            ValueDatum ave = Bar.Sma(mapping, barData);

            string propertyBase = mapping.Property;

            properties.Where(p => p.Name == propertyBase)
                .First()
                .SetValue(Datum, ave.Value);

            properties.Where(p => p.Name == $"D{propertyBase}")
                .First()
                .SetValue(Datum, ave.Slope);

            properties.Where(p => p.Name == $"D2{propertyBase}")
                .First()
                .SetValue(Datum, ave.DSlope);
        }
    }
}
