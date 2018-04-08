using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace Felix.Library.BLL
{
    public class WaveStack
    {
        private Stack<WaveBuilderItem> Stack { get; set; }

        public WaveStack ()
        {
            Stack = new Stack<WaveBuilderItem>();
        }

        public WaveBuilderItem Top()
        {
            WaveBuilderItem item = Stack.Peek();

            if (item == null)
            {
                item = new WaveBuilderItem();

                Stack.Push(item);
            }

            return item;
        }


    }
}
