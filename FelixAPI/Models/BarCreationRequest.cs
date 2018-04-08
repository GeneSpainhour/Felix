using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Data;

namespace FelixAPI.Models
{
    public class BarCreationRequest
    {
        public string Symbol { get; set; }
        public Bar Bar { get; set; }

        public BarCreationRequest() { }

        public BarCreationRequest(string symbol, Bar bar)
        {
            Symbol = symbol;

            Bar = bar;
        }
    }
}
