using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;



namespace Felix.MarketData.Repositories
{
    public class MarketRepository : IMarketRepository
    {
        private FelixMarketEntities Context { get; set; }

        private object mux ;
        public MarketRepository()
        {
            Context = new FelixMarketEntities();

            mux = new object();
        }

        public IEnumerable<Market> Markets()
        {
            return Context.Markets;
        }

        public Market Market(int marketId)
        {
            var market = Context.Markets.FirstOrDefault(m => m.MarketId == marketId);

            if (market != null)
            {
                market.Contracts = null;
            }

            return market;
        }

        public Market Market(string symbol)
        {
            Market market = Context.Markets
                .Include (m => m.MetaMappings)
                .FirstOrDefault(m => m.Symbol.Equals(symbol));

            var contracts = Contracts(market.MarketId).ToList();

            contracts.ForEach(c => c.Market = null);

            market.Contracts = (ICollection<Contract>)contracts;

            return market;
        }

        public IEnumerable<MetaMapping> MetaMappings()
        {
            return Context.MetaMappings.ToList();
        }

        public IEnumerable<MetaMapping> MetaMappings(int marketId)
        {
            return Context.MetaMappings.Where(m => m.MarketId == marketId);
        }

        public IEnumerable<Bar> Bars(int contractId)
        {
            return Context.Bars.Where(b => b.ContractId == contractId);
        }

        public IEnumerable<Bar> Bars(string symbol, DateTime startDate, DateTime endDate)
        {

            Contract contract = Contract(symbol);

            if (contract == null)
            {
                throw new ArgumentException($"Couldn't find contract with symbol: {symbol}");
            }

            return Context.Bars.Where(b => b.ContractId == contract.ContractId
              && b.Time > startDate
              && b.Time < endDate);
        }

        public IEnumerable<Bar> Bars(int period, string symbol)
        {
            var barQuery = ( from c in Context.Contracts
                             join b in Context.Bars on c.ContractId equals b.ContractId
                             where c.Symbol.Equals(symbol) && b.Period == period
                             select b );
            return barQuery;
        }

        public Bar Bar (string symbol, DateTime time)
        {
            Bar bar = null;

            int? contractId = Context.Contracts.FirstOrDefault(c => c.Symbol == symbol)?.ContractId;

            if (contractId != null)
            {
                bar = Context.Bars.FirstOrDefault(b => b.ContractId == contractId.Value && b.Time == time);
            }

            return bar;
        }

        public Bar Bar (int barId)
        {
            return Context.Bars.FirstOrDefault(b => b.BarId == barId);
        }

        public void AddBar (Bar bar, bool saveAfterAdding=true)
        {
            lock (mux)
            {
                var existingBar = Context.Bars
                    .FirstOrDefault(b => b.ContractId == bar.ContractId
                    && b.Period == bar.Period
                    && b.Time == bar.Time);

                if (existingBar == null)
                {
                    Context.Bars.Add(bar);

                    if (saveAfterAdding)
                    {
                        Context.SaveChanges();
                    }
                } 
            }
          
        }

        public IEnumerable<Contract> Contracts(int marketId)
        {
            return Context.Contracts.Where(c => c.MarketId == marketId);
        }

        public IEnumerable<Contract> Contracts(string symbol)
        {
            Market market = Market(symbol);

            return market.Contracts;
                
        }

        public Contract Contract(string symbol)
        {
            var contract = Context.Contracts.FirstOrDefault(c => c.Symbol.Equals(symbol));

            if (contract != null)
            {
                var market = Market(contract.MarketId);

                contract.Market = market;
            }

        
            return contract;
        }

        public void AddContract(Contract newContract)
        {
            Context.Contracts.Add((Contract)newContract);

            Context.SaveChanges();

            if (newContract.Bars.Any())
            {
                foreach(var bar in newContract.Bars)
                {
                    bar.ContractId = newContract.ContractId;

                    AddBar(bar, false);
                }

                Context.SaveChanges();
            }
        }

        public Trend Trend (int trendId)
        {
            return Context.Trends.FirstOrDefault(t => t.TrendId == trendId);
        }

        public IEnumerable<Move> Moves(int contractId, int period, DateTime time)
        {
            return Context.MovesGet(contractId, period, time);
        }
    }
}
