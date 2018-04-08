using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Felix.MarketData.Repositories;

namespace Felix.Library.BLL
{
    public class BarDomainObject : DomainObject, IBarDomainObject
    {
        private static ContractCache ContractCache = new ContractCache();

        public BarDomainObject(): base() { }
        public int Save(Felix.Interfaces.IBar bar)
        {
            var newBar = AutoMapper.Mapper.Map<Felix.Interfaces.IBar, Felix.MarketData.Bar>(bar);

            if (newBar != null)
            {
                Repository.AddBar(newBar);

                bar.BarId = newBar.BarId;
            }

            return newBar.BarId;
        }

        public async Task<int> Save (string symbol, Felix.Interfaces.IBar bar)
        {
            var mappedBar = AutoMapper.Mapper.Map<Felix.Interfaces.IBar, Felix.MarketData.Bar>(bar);

            try
            {
                var contract = ContractCache[symbol];

                if (contract != null)
                {
                    mappedBar.ContractId = contract.ContractId;

                    ParameterizedThreadStart update = obj =>
                    {
                        MarketData.Bar entry = obj as MarketData.Bar;

                        Repository.AddBar(entry);
                    };

                    new Thread(update).Start(mappedBar);

                }
            
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");

                throw e;
            }

            return await Task.FromResult(mappedBar.BarId);
        }

    }
}
