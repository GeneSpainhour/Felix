using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class Market: IMarket
    {

        public int MarketId { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal TickSize { get; set; }
        public string Months { get; set; }

        public IEnumerable<IContract> Contracts { get; set; }

        public IEnumerable<IMetaMapping> MetaMappings { get; set; }

        public Market ()
        {
            Contracts = new List<IContract>();

            MetaMappings = new List<IMetaMapping>();
        }
    }
}
