using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.BarMessaging;
using Felix.Interfaces;
using Felix.Messaging.Pipes;
using Felix.Models;
using Newtonsoft.Json;

namespace Felix.Utilities.Services.BarService
{
    public class BarService
    {
        private static string BarPipeServer = ".";
        private static string BarPipeServerName = "Felix.BarMessagingService";

        static DuplexClientPipe pipe;
        static BarService()
        {
            pipe = new DuplexClientPipe(BarPipeServer, BarPipeServerName);
        }
        public static async Task AddBar (string symbol, IBar bar)
        {
            BarAddAction action = new BarAddAction( new BarAddPayload(symbol, (Felix.Data.Bar)bar));

      //      DuplexClientPipe pipe = new DuplexClientPipe(BarPipeServer, BarPipeServerName);

            var msg = JsonConvert.SerializeObject(action);

            await pipe.Send(msg);

          //  pipe.Close();

        }
    }
}
