using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FelixConsole
{
    public class AsyncTester
    {
        public static void Test ()
        {

            ProcessFile();

            ProcessFileFromNew();
        //    Task t1 = CallProcess();

            //    InTheMeanTime();

            //    var content = ReadFile().Result;

            //    Console.WriteLine($"file returned {content.Length} characters");



            //    t1.Wait();
        }

        private static void InTheMeanTime()
        {
            Report("While waiting I thought I'd do this");

            List<string> lst = new List<string>();

            for (int i = 0; i< 10;  ++i)
            {
                lst.Add("*");

                lst.ForEach(t => Report(t));
            }
        }

        private static void ProcessFile ()
        {
            Task.Run(() => 
            {
                var content = ReadFile().Result;

                Report($"Content size {content.Length} ");
            }).ConfigureAwait(false);
        }

        private static void ProcessFileFromNew()
        {
            Task.Factory.StartNew(() =>
            {
                var content = ReadFile().Result;

                Report($"Content size {content.Length} ");
            }).ConfigureAwait(false);
        }

        private static void Report (string msg)
        {
            Console.WriteLine(msg);
        }

        private static Task LongProcess()
        {
            return Task.Run(
                () =>
                {
                    System.Threading.Thread.Sleep(1000);
                });
        }

        private static async Task CallProcess()
        {
            DateTime now = DateTime.Now;

            await LongProcess();

            DateTime later = DateTime.Now;

            Console.WriteLine($"{later - now} elapsed");

        }

        private static async Task<string> ReadFile()
        {
            string fileName = @"E:\Felix\Felix.Data\stats.json";

            string content = string.Empty;

            using (var rdr = File.OpenText(fileName))
            {
                content = await rdr.ReadToEndAsync(); 
            }

            return content;
        }
    }
}
