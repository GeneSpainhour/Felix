using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class MetaMapping : IMetaMapping
    {
        public int MetaMappingId { get; set; }

        public int MarketId { get; set; }
        public Nullable<int> Value { get; set; }

        public Nullable<double> DValue { get; set; }

        public string Property { get; set; }

        public MetaMapping() { }

        public MetaMapping(int metaMappingId, int marketId, string property, int? value,  double? dValue)
        {
            MetaMappingId = metaMappingId;

            MarketId = marketId;

            Value = value;

            Property = property;

            DValue = dValue;
        }
    }
}
