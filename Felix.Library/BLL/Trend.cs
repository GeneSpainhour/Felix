using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Library.BLL
{
    public class Trend: DomainObject
    {


        public int TrendId { get; set; }

        public string Description { get; set; }

        public Trend() { }

        public Trend(Felix.Data.Trend.TrendType type): this()
        {
            var t = Repository.Trend((int)type);
            TrendId = t.TrendId;
            Description = t.Description;
        }
    }
}
