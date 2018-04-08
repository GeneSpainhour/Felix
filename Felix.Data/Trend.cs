using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Data
{
    public class Trend
    {
        public enum TrendType
        {
            Unknown = 1,

            Up = 2,

            Down = 3

        }

        public int TrendId { get; set; }

        public string Description { get; set; }

        public Trend() { }

        public Trend(int trendId, string description)
        {
            TrendId = trendId;

            Description = description;
        }
    }
}
