using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Messaging.Interfaces
{
    public interface IActionDispatcher
    {
        Task<string> Dispatch(IAction action);
    }
}
