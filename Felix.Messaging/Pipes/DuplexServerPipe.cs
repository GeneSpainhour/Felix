using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;
using Newtonsoft.Json;

namespace Felix.Messaging.Pipes
{
    public class DuplexServerPipe : ConsoleReporter
    {
        private NamedPipeServerStream Stream { get; set; }

        public DuplexServerPipe(NamedPipeServerStream stream): base()
        {
            Stream = stream;
        }

        public async Task Run(IActionDispatcher dispatcher)
        {
            while (true)
            {
                IAction action = await Receive();

                //  Report($"Server received: {action.Type}");

                string msg = await dispatcher.Dispatch(action);

                await Send(msg); 
            }
        }

        public async Task<Messages.Actions.Action> Receive()
        {
            byte[] byteBuffer = new byte[1024];

            int bytesRead = await Stream.ReadAsync(byteBuffer, 0, byteBuffer.Length);

            string tempString = Encoding.UTF8.GetString(byteBuffer, 0, bytesRead);

            return JsonConvert.DeserializeObject< Messages.Actions.Action>(tempString);
        }

        public async Task Send(string msg)
        {
         //   Report($"Server Sending {msg}");

            byte[] byteBuffer = Encoding.UTF8.GetBytes(msg);

            await Stream.WriteAsync(byteBuffer, 0, byteBuffer.Length);
        }

        public void Terminate()
        {
        //    Report($"Server Teminating");

            Stream.Disconnect();

            Stream.Dispose();

        }
    }
}
