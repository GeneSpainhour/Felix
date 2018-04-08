using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Messaging.Pipes
{
    public class DuplexClientPipe : ConsoleReporter
    {
        private NamedPipeClientStream Stream { get; set; }

        public DuplexClientPipe(NamedPipeClientStream stream) : base()
        {
            Stream = stream;
        }

        public DuplexClientPipe(string serverName, string pipeName)
        {
            Stream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut);

            Stream.Connect();
        }

        public void Close()
        {
            if (Stream != null)
            {
                Stream.Close();
            }
        }

        public async Task<string> Receive()
        {
            byte[] byteBuffer = new byte[1024];

            StringBuilder bldr = new StringBuilder();

            while (true)
            {
                int bytesRead = await Stream.ReadAsync(byteBuffer, 0, byteBuffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }

                string tempString = Encoding.UTF8.GetString(byteBuffer, 0, bytesRead);

                bldr.Append(tempString);
            }

          //  Report($"Client Recieved {bldr.ToString()}");

            return bldr.ToString();
        }

        public async Task Send(string msg)
        {
          //  Report($"Client Sending {msg}");

            if (!Stream.IsConnected)
            {
                Stream.Connect();
            }

            byte[] byteBuffer = Encoding.UTF8.GetBytes(msg);

            await Stream.WriteAsync(byteBuffer, 0, byteBuffer.Length);
        }
    }
}
