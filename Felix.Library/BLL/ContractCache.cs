using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Library.BLL
{
    public class ContractCache: DomainObject
    {
        private Dictionary<string, Felix.MarketData.Contract> Contracts = new Dictionary<string, Felix.MarketData.Contract>();

        public ContractCache(): base() { }

        public Felix.MarketData.Contract this [string symbol]
        {
            get
            {
                Felix.MarketData.Contract contract = null;

                if (!Contracts.ContainsKey(symbol))
                {
                    contract = Repository.Contract(symbol);

                    if (contract == null)
                    {
                        throw new ArgumentException($"Unable to find contract with symbol: {symbol}");
                    }

                    Contracts.Add(symbol, contract);
                }
                else
                {
                    contract = Contracts[symbol];
                }

                return contract;
            }
        }
    }
}
