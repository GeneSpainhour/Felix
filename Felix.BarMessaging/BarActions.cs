using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Felix.BarMessaging
{
    public class BarAddAction : Felix.Messaging.Messages.Actions.Action
    {
        public BarAddAction(BarAddPayload payload) :
            base("Bar.Add", JsonConvert.SerializeObject(payload))
        { }
    }

}
