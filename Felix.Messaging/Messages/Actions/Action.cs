using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;

namespace Felix.Messaging.Messages.Actions
{
    public class Action : IAction
    {
        public string Type { get; set; }
        public string Payload { get; set; }

        public Action(string type, string payload)
        {
            Type = type;

            Payload = payload;
        }
    }
}
