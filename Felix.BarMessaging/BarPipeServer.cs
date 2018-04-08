using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;
using Felix.Messaging.Pipes;

namespace Felix.BarMessaging
{
    public class BarPipeServer
    {
        IReducerContext Context { get; set; }

        private string ServerName = "Felix.BarMessagingService";

        private ServerPipe serverPipe;

        public BarPipeServer ()
        {
            Context = BarMessagingContext.ReducerContext;

            serverPipe = new ServerPipe(ServerName, Context);

            serverPipe.Start();

            

        }
    }
}
