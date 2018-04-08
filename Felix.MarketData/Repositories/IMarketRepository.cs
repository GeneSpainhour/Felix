using System;
using System.Collections.Generic;

namespace Felix.MarketData.Repositories
{
    public interface IMarketRepository
    {
        void AddBar(Bar bar, bool saveAfterAdding = true);
        void AddContract(Contract newContract);
        Bar Bar(int barId);
        Trend Trend(int trendId);
        IEnumerable<Bar> Bars(int contractId);
        IEnumerable<Bar> Bars(int period, string symbol);
        IEnumerable<Bar> Bars(string symbol, DateTime startDate, DateTime endDate);
        Bar Bar(string symbol, DateTime time);
        Contract Contract(string symbol);
        IEnumerable<Contract> Contracts(int marketId);
        IEnumerable<Contract> Contracts(string symbol);
        Market Market(int marketId);
        Market Market(string symbol);
        IEnumerable<Market> Markets();
        IEnumerable<MetaMapping> MetaMappings(int marketId);

        IEnumerable<MetaMapping> MetaMappings();

        IEnumerable<Move> Moves(int contractId, int period, DateTime time);
    }
}