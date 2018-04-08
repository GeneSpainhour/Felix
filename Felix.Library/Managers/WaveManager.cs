using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Library.BLL;

namespace Felix.Library.Managers
{

    public class WaveManager
    {
        private WaveBuilder builder { get; set; }

        public WaveManager()
        {
            builder = new WaveBuilder();
        }

        public void Insert( IMove move)
        {
            builder.Insert(move);
        }
    }
}
