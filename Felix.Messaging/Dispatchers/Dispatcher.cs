using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;

namespace Felix.Messaging.Dispatchers
{
    public class Dispatcher : IActionDispatcher
    {
        IDictionary<string, IActionReducer> Reducers { get; set; }

        public Dispatcher(IDictionary<string, IActionReducer> reducers)
        {
            Reducers = reducers;
        }

        public async Task<string> Dispatch(IAction action)
        {
            string msg = string.Empty;

            IActionReducer reducer = GetReducer(action);

            msg = await reducer.Reduce(action);

            return msg;
        }

        private IActionReducer GetReducer(IAction action)
        {
            IActionReducer retReducer = null;

            if (Reducers.Keys.Contains(action.Type))
            {
                retReducer = Reducers[action.Type];
            }

            return retReducer;
        }
    }
}
