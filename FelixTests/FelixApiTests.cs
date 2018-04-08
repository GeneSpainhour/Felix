using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FelixTests
{
    [TestClass]
    public class FelixApiTests
    {
        HttpClient client = new HttpClient();

        [TestMethod]
        public void AddBarTest ()
        {
            client.BaseAddress = new Uri("http://localhost:9000/");

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            IBar bar = BarMocks.UpMove(1, (double)100).ToList().First();

            int newId = CreateBar(bar).Result;

            Assert.IsTrue(newId > 0);
            
        }

        private async Task<int> CreateBar (IBar bar)
        {
            int newId = int.MinValue;

            BarCreationRequest request = new BarCreationRequest("YMH18", bar);

            HttpResponseMessage response = await client.PostAsJsonAsync("api/bar", request);

            response.EnsureSuccessStatusCode();

            newId = await response.Content.ReadAsAsync<int>();

            return newId;
        }
    }

    public class BarCreationRequest
    {
        public string Symbol { get; set; }
        public IBar Bar { get; set; }

        public BarCreationRequest() { }

        public BarCreationRequest(string symbol, IBar bar)
        {
            Symbol = symbol;

            Bar = bar;
        }
    }
}
