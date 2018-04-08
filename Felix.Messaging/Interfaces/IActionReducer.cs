using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Messaging.Interfaces
{
    public interface IActionReducer
    {
        Task<string> Reduce(IAction action);
    }
}
