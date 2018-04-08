using System.Collections.Generic;

namespace Felix.Interfaces
{
    public interface IMarket
    {
        IEnumerable<IContract> Contracts { get; set; }

        IEnumerable<IMetaMapping> MetaMappings { get; set; }
        string Exchange { get; set; }
        int MarketId { get; set; }
        string Months { get; set; }
        string Name { get; set; }
        string Symbol { get; set; }
        decimal TickSize { get; set; }
    }
}