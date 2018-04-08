using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;

namespace Felix.Messaging.Messages.Reducers
{
    public abstract class ActionReducer : IActionReducer
    {
        public abstract Task<string> Reduce(IAction action);
    }
}
