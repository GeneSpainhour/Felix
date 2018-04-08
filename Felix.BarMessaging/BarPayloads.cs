using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Data;
using Felix.Interfaces;

namespace Felix.BarMessaging
{
    public class BarAddPayload
    {
        public string Symbol { get; set; }
        public Bar Bar { get; set; }

        public BarAddPayload() { }
        public BarAddPayload(string symbol, Bar bar)
        {
            Symbol = symbol;

            Bar = bar;
        }
    }

    public class BarStatePayload
    {
        public bool Result { get; set; }

        public int BarId { get; set; }

        public BarStatePayload(int barId, bool result)
        {
            BarId = barId;

            Result = result;
        }
    }
}
