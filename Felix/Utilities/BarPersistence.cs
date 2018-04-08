using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Felix.Data;
using Felix.Models;
using Felix.Tools;
/*
 *  Reference:
 *  E:\Essays\ReactiveEssay- ParameterizedThreadStart
 * 
 * 
 */
namespace Felix.Utilities
{
    public interface IBarPersistence
    {
        void Start();

        void Stop();

        void Add(BarCreationRequest request);

        Task AddBarAsync(BarCreationRequest request);

        ManualResetEvent DoneEvent { get; }
    }
    public class BarPersistence : IBarPersistence
    {
        SynchronizedQueue Queue { get; set; }

        Thread LoaderThread;

        IFelixHttpClient Client { get; set; }

        public BarPersistence(IFelixHttpClient client)
        {
            Queue = new SynchronizedQueue();

            LoaderThread = new Thread(Loader);

            Client = client;

            DoneEvent = new ManualResetEvent(false);
        }

        public void Start()
        {
            Queue.Events.Start();

            LoaderThread.Start();
        }

        public void Stop()
        {
            Queue.Events.Stop();


        }
        public void Add(BarCreationRequest request)
        {
            Queue.Push(request);
        }

        public async Task AddBarAsync(BarCreationRequest request)
        {
            IFelixHttpClient client = new FelixHttpClient();

            try
            {
                int barId = await client.CreateBar(request);

                Debug.WriteLine($"barId: {barId}");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error: {e.Message}");

                Debugger.Break();
            }
        }

        public ManualResetEvent DoneEvent { get; }

        void Report(string msg)
        {
            Debug.WriteLine(msg);
        }

        void Loader()
        {
            Report($"Entering Loader");

            while (true)
            {
                if (!Queue.IsRunning)
                {
                    Report($"RunEvent Reset. Shutting down ");
                    break;
                }

                if (Queue.WaitForContent())
                {
                    Report($"Queue has content. Executing save");

                    ExecuteSave();
                }

            }

            DrainQueue();

            DoneEvent.Set();
        }

        void DrainQueue()
        {

            Report($"Draining Queue");

            while (Queue.HasContent)
            {
                ExecuteSave();
            }
        }

        void ExecuteSave()
        {
            BarCreationRequest request = Queue.Pop() as BarCreationRequest;

            int barId = Client.CreateBar(request).Result;
        }
    }
}
