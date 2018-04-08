using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;

namespace Felix.Messaging.Messages.State
{
    public class State: IState
    {
        public string Type { get; set; }

        public string Payload { get; set; }

        public State() { }

        public State(string type, string payload)
        {
            Type = type;

            Payload = payload;
        }
    }
}
