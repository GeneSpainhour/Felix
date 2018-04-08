using System.Collections.Generic;
using Felix.Messaging.Interfaces;

namespace Felix.Messaging.Interfaces
{
    public interface IReducerContext
    {
        IDictionary<string, IActionReducer> Items { get; }
    }
}