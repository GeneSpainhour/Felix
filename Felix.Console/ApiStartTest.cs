using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelixConsole
{
    public class ApiStartTest
    {
        private static string FelixApiName = "FelixApi";

      

        public static void Test()
        {
            string procPath = ConfigurationManager.AppSettings[FelixApiName];

            if (!ProcessIsRunning(FelixApiName))
            {
                Report($"Process, {FelixApiName} is not running. Starting");

                bool procStarted = StartProcess(procPath, FelixApiName);

                Report($"Process, {FelixApiName} is running: {procStarted}. ");
            }
            else
            {
                Report($"Process, {FelixApiName} is running.");
            }

        }

        static bool StartProcess(string procPath, string procName)
        {
            Process.Start(procPath);

            return ProcessIsRunning(procName);
        }

        private static bool ProcessIsRunning (string procName)
        {
            Process proc = Process.GetProcessesByName(procName).ToList().FirstOrDefault();

            return proc != null;
        }

        private static void Report (string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
