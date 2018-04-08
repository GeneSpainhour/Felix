using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Messaging.Interfaces
{
    public interface IState
    {
        string Type { get; set; } // the state type

        string Payload { get; set; }
    }
}
