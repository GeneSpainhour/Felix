using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Models
{

    public class BarCreationRequest
    {
        public string Symbol { get; set; }
        public IBar Bar { get; set; }

        public BarCreationRequest() { }

        public BarCreationRequest(string symbol, IBar bar)
        {
            Symbol = symbol;

            Bar = bar;
        }
    }
}
