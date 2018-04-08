using System.Collections.Generic;


namespace Felix.Interfaces
{
    public interface IMarketRepository
    {
        void AddBar(IBar bar, bool saveAfterAdding = true);
        void AddContract(IContract newContract);
        IBar Bar(int barId);
        IEnumerable<IBar> Bars(int contractId);
        IEnumerable<IBar> Bars(int period, string symbol);
        IContract Contract(string symbol);
        IEnumerable<IContract> Contracts(int marketId);
        IMarket Market(int marketId);
        IMarket Market(string symbol);
        IEnumerable<IMarket> Markets();
    }
}