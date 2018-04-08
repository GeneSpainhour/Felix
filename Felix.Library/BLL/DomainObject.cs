using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.MarketData.Repositories;

namespace Felix.Library.BLL
{
    public class DomainObject
    {
        protected IMarketRepository Repository { get; set; }

        public DomainObject ()
        {
            Repository = new MarketRepository();
        }

        static DomainObject()
        {
            AutoMapperConfig.Register();
        }
    }
}
