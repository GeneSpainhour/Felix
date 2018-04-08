using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;
using Felix.Messaging.Messages.Reducers;

namespace Felix.Messaging.Pipes
{
    public class ServerPipe : ConsoleReporter
    {
        private string ServerName { get; set; }

        private NamedPipeServerStream Pipe { get; set; }

        private IReducerContext Reducers { get; set; }

        private IActionDispatcher Dispatcher { get; set; }

        public ServerPipe(string name, IReducerContext reducerContext ): base()
        {
            ServerName = name;

            Reducers = reducerContext;

            Dispatcher = new Dispatchers.Dispatcher(Reducers.Items);
        }

        public void Start()
        {
            Task.Run(async () => 
            {
                try
                {
                    while (true)
                    {
                        Pipe = new NamedPipeServerStream(ServerName);

                        Report($"Pipe Server: {ServerName} created\nWaiting for connection");

                        Pipe.WaitForConnection();

                        var pipe = new DuplexServerPipe(Pipe);

                        await pipe.Run(Dispatcher);

                        pipe.Terminate();
                    }
                }
                catch (Exception e)
                {
                    Report($"Error: {e.Message}");
                }
            });
        }

        public void _Start()
        {
            var tcs = new TaskCompletionSource<object>();

            ThreadPool.QueueUserWorkItem( async _ =>  {
                try
                {
                    while (true)
                    {
                        Pipe = new NamedPipeServerStream(ServerName);

                        Report($"Pipe Server: {ServerName} created\nWating for connection");

                        Pipe.WaitForConnection();

                        var pipe = new DuplexServerPipe(Pipe);

                        await pipe.Run(Dispatcher);

                        pipe.Terminate();
                    }
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
        }
    }
}
