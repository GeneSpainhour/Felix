using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Felix.Messaging.Messages.Clients.BarClient
{
    public class BarState : Felix.Messaging.Messages.State.State
    {
        public BarState(BarStatePayload payload)
             : base("BarState", JsonConvert.SerializeObject(payload)) { }
    }
}
