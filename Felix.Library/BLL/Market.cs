using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.MarketData;
using Felix.MarketData.Repositories;
using AutoMapper;

namespace Felix.Library.BLL
{
    public class Market : DomainObject, IMarket
    {
        public int MarketId { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal TickSize { get; set; }
        public string Months { get; set; }

        public IEnumerable<IContract> Contracts { get; set; }

        public IEnumerable<IMetaMapping> MetaMappings { get; set; }

        public Market():base()
        {
            Contracts = new HashSet<IContract>();

            MetaMappings = new HashSet<IMetaMapping>();
        }

        public Market (string symbol ): this()
        {
            var market = Repository.Market(symbol);

            MarketId = market.MarketId;

            Name = market.Name;

            Exchange = market.Exchange;

            Symbol = market.Symbol;

            TickSize = market.TickSize;

            Months = market.Months;

            MetaMappings = Mapper.Map<ICollection<Felix.MarketData.MetaMapping>, IEnumerable<IMetaMapping>>(market.MetaMappings);

            Contracts = Mapper.Map<ICollection<Felix.MarketData.Contract>, IEnumerable<IContract>>( market.Contracts);

            
        }
    }
}
