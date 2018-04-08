using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Felix.Interfaces;
using Felix.MarketData;
using Felix.MarketData.Repositories;

namespace Felix.Library.BLL
{
    public class Contract : DomainObject, IContract
    {
        public int ContractId { get; set; }
        public int MarketId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public IEnumerable<Felix.Interfaces.IBar> Bars { get; set; }
        public  IMarket Market { get; set; }

        public Contract():base()
        {
            Bars = new HashSet<Felix.Interfaces.IBar>();
        }

        public Contract ( string symbol ) : this()
        {


            var contract = Repository.Contract(symbol);

            ContractId = contract.ContractId;

            MarketId = contract.MarketId;

            Market = Mapper.Map<IMarket>(contract.Market);

            Name = contract.Name;

            Symbol = contract.Symbol;

            BeginDate = contract.BeginDate;

            EndDate = contract.EndDate;


        }
    }
}
