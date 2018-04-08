using System;

namespace Felix.Interfaces
{
    public interface IMetaMapping
    {
        int MetaMappingId { get; set; }
        int MarketId { get; set; }
        Nullable<int> Value { get; set; }

        Nullable<double> DValue { get; set; }
        string Property { get; set; }
    }
}