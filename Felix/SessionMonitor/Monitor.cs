using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Data;
using Felix.Data;
using Felix.Managers;

namespace Felix.SessionMonitor
{
    public class TimeDimension
    {
        int Period { get; set; }

        public MoveManager MoveMgr;

        public PositionManager PosMgr;

        public TimeDimension(int period)
        {
            MoveMgr = new MoveManager();

            PosMgr = new PositionManager(MoveMgr);
        }

      

        public void Insert(BarDatum datum)
        {
            MoveMgr.Insert(datum);

            // analyzer.update(Entries);
        }
    }
    public class Monitor
    {
        public List<TimeDimension> Dimensions { get; set; }
        public Monitor()
        {
            Dimensions = new List<TimeDimension>() { new TimeDimension(5), new TimeDimension(1), new TimeDimension(15)};
        }

        public void Insert (int index, BarDatum data)
        {
            var dim = Dimensions[index];

            dim.Insert(data);
        }



   
    }
}
