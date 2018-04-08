using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Library.Analyzers
{
    public interface IProspectiveIR
    {
        double Degrees { get; }
        double Price { get; }
    }
    public class ProspectiveIR: IProspectiveIR
    {
        public double Degrees { get; set; }

        public double Price { get; set; }

        public ProspectiveIR(double degrees, double price)
        {
            Degrees = degrees;

            Price = price;
        }

    }
}
