using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Models;
using Felix.Tools;

namespace Felix.Utilities
{
    public interface IPersistence
    {
       void  PersistBar(BarCreationRequest request);

        void PersistBarAsTask(BarCreationRequest request);

        void Stop();
    }
    public class Persistence : IPersistence
    {
        // Ensure FelixApi.exe is running
        // Create HttpClient
        private string FelixApi = "FelixApi";

        private string FelixApiPath = @"E:\Felix\FelixAPI\bin\Debug\FelixApi.exe";
        IFelixHttpClient httpClient { get; set; }
        
        string ApiPath { get; set; }
        bool ApiIsRunning { get; set; }

        private IBarPersistence BarPersistence { get; set; }

        public Persistence(bool IsAsync=false)
        {
            httpClient = new FelixHttpClient();

            StartAPIRunning();

            BarPersistence = new BarPersistence(httpClient);

            if (!IsAsync)
            {
                BarPersistence.Start();
            }
            
        }

        public void Stop ()
        {
            BarPersistence.Stop();

            BarPersistence.DoneEvent.WaitOne(-1);
        }

        private bool StartAPIRunning()
        {
            if (!ProcessIsRunning(FelixApi))
            {
                string procPath = FelixApiPath;

                if (!StartProcess(procPath, FelixApi))
                {
                    throw new Exception($"Unable to start process {FelixApi}");
                }
            }

            ApiIsRunning = true;

            return ApiIsRunning;
        }

        bool StartProcess(string procPath, string procName)
        {
            Process.Start(procPath);

            return ProcessIsRunning(procName);
        }

        private bool ProcessIsRunning(string procName)
        {
            Process proc = Process.GetProcessesByName(procName).ToList().FirstOrDefault();

            return proc != null;
        }

        public void PersistBar(BarCreationRequest request)
        {
            BarPersistence.Add(request); 
        
        }

        public void PersistBarAsTask(BarCreationRequest request)
        {
            Task.Run(() => 
            {
                BarPersistence.AddBarAsync(request).Wait();
            }).Wait();
        }
    }
}
