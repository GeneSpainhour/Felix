using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Messaging.Messages.Clients.BarClient
{
    public class BarAddPayload
    {
        public int BarId { get; set; }
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }

        public BarAddPayload() { }
        public BarAddPayload(int barId, DateTime time, double open, double high, double low, double close)
        {
            BarId = barId;

            Time = time;

            Open = open;

            High = high;

            Low = low;

            Close = close;
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
