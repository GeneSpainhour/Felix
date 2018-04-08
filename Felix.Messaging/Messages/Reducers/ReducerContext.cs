using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;

namespace Felix.Messaging.Messages.Reducers
{
    public class ReducerContext : IReducerContext
    {
        private IDictionary<string, IActionReducer> Reducers { get; set; }

        public ReducerContext() { }

        public ReducerContext(IDictionary<string, IActionReducer> reducers)
        {
            Reducers = reducers;
        }

        public IDictionary<string, IActionReducer> Items => Reducers;
    }
}
