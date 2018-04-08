using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Felix.BarMessaging;
using Felix.Interfaces;
using Felix.Utilities.Services.BarService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FelixTests
{
    [TestClass]
    public class FelixMessagingTests
    {
        IBar bar = BarMocks.UpMove(1, (double)100).ToList().First();

        string symbol = "YMM18";

        [TestMethod]
        public async Task BarServerTest()
        {
            BarPipeServer server = new BarPipeServer();

            bar.BarId = 0;

            bar.Period = 100;

            await BarService.AddBar(symbol, bar);

            Thread.Sleep(30000);

        }

        [TestMethod]
        public async Task MultiBarServerTest()
        {
            List<IBar> bars = BarMocks.UpMove(1000, (double)100).ToList();

            BarPipeServer server = new BarPipeServer();

            int i = 0;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            foreach (var bar in bars)
            {
                bar.BarId = 0;

                bar.Period = 100;
              
                BarService.AddBar(symbol, bar);

             
            }

            stopwatch.Stop();

            Debug.WriteLine($"this took {stopwatch.ElapsedMilliseconds}");

            Thread.Sleep(15000);
        }

     
      
    }
}
