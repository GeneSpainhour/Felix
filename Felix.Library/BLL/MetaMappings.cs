using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Felix.Interfaces;

namespace Felix.Library.BLL
{
    public class MetaMappings : DomainObject, IMetaMappings
    {
        public IEnumerable<IMetaMapping> Mappings { get; }

        public MetaMappings(): base()
        {
            var marketMappings = Repository.MetaMappings().ToList();

            Mappings = AutoMapper.Mapper.Map<List<MarketData.MetaMapping>, List<IMetaMapping>>(marketMappings);
        }

        public IEnumerable<IMetaMapping> AveMappings (int marketId)
        {
            return Mappings.Where(m => m.MarketId == marketId && m.Property.Contains("A"))
                .ToList();
        }

        public IEnumerable<IMetaMapping> this [int marketId]
        {
            get
            {
                return Mappings.Where(m => m.MarketId == marketId).ToList();
            }
        }

        
    }
}
