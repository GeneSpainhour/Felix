using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Interfaces
{
    public interface IBar
    {
        int BarId { get; set; }
        int ContractId { get; set; }
        int Period { get; set; }
        double Close { get; set; }
        double High { get; set; }
        int Index { get; set; }
        double Low { get; set; }
        double Open { get; set; }
        DateTime Time { get; set; }
        Nullable<int> Volume { get; set; }
      
    }
}
