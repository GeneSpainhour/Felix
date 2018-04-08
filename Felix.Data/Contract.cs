using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class Contract: IContract
    {
        public int ContractId { get; set; }
        public int MarketId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime EndDate { get; set; }

        public IEnumerable<IBar> Bars { get; set; }

        public IMarket Market { get; set; }
    }
}
