using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Library.BLL
{
    public interface IBarDomainObject
    {
        int Save(Interfaces.IBar bar);

        Task<int> Save(string symbol, Interfaces.IBar bar);
    }
}