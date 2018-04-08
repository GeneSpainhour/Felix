using System;
using System.Collections.Generic;

namespace Felix.Interfaces
{
    public interface IContract
    {
        IEnumerable<IBar> Bars { get; set; }
        DateTime BeginDate { get; set; }
        int ContractId { get; set; }
        DateTime EndDate { get; set; }
        IMarket Market { get; set; }
        int MarketId { get; set; }
        string Name { get; set; }
        string Symbol { get; set; }
    }
}