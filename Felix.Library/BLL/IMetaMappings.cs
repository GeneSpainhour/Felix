using System.Collections.Generic;
using Felix.Interfaces;

namespace Felix.Library.BLL
{
    public interface IMetaMappings
    {
        IEnumerable<IMetaMapping> this[int marketId] { get; }
        IEnumerable<IMetaMapping> AveMappings(int marketId);
        IEnumerable<IMetaMapping> Mappings { get; }
    }
}