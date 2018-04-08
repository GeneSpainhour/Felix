using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Tools;
using Felix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FelixTests
{
    [TestClass]
    public class FelixTests
    {
        IPersistence persister = new Persistence(true);

        [TestMethod]
        public void PersistenceTest()
        {
            IPersistence persister = new Persistence();

            List<IBar> bars =  BarMocks.UpMove(1000, (double)100);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            foreach (var bar in bars)
            {
                bar.BarId = 0;

                bar.Period = 100;

                Felix.Models.BarCreationRequest request = new Felix.Models.BarCreationRequest("YMH18", bar);

                persister.PersistBar(request);
            }

            stopwatch.Stop();

            Debug.WriteLine($"adding {bars.Count} bars took {stopwatch.ElapsedMilliseconds}");

            persister.Stop();

            //DateTime later = DateTime.Now;

            //Debug.WriteLine($"{bars.Count} bars took {later-now}");
        }

        [TestMethod]
        public async Task PersistAsyncTest()
        {
            var t = Task.Run(() =>
            {
                try
                {
                    List<IBar> bars = BarMocks.UpMove(1, (double)100);

                    DateTime now = DateTime.Now;

                    Felix.Models.BarCreationRequest request = new Felix.Models.BarCreationRequest("YMH18", bars.First());

                    persister.PersistBarAsTask(request);
                }
                catch (Exception e)
                {

                    Debug.WriteLine($"{e.Message}");

                    Debugger.Break();
                }
            });

            await t;
        }

        [TestMethod]
        public void ClientTest()
        {
            IBar bar = BarMocks.UpMove(1, (double)100).ToList().First();

            Felix.Models.BarCreationRequest request = new Felix.Models.BarCreationRequest("YMH18", bar);

            IFelixHttpClient client = new FelixHttpClient();

            int barId = client.CreateBar(request).Result;

            Assert.IsTrue(barId > 0);
        }
    }
}
