using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Models;
using Newtonsoft.Json;

namespace Felix.Tools
{
    public interface IFelixHttpClient
    {
        Task<int> CreateBar(BarCreationRequest request);
    }
    public class FelixHttpClient: IFelixHttpClient
    {
        private HttpClient Client { get; set; }

        private string UrlKey = "FelixUrl";

        private string FelixUrl = "http://localhost:9000";

        
        public FelixHttpClient()
        {
            Client = new HttpClient();

            Client.BaseAddress = new Uri(FelixUrl);

            Client.DefaultRequestHeaders.Clear();

            Client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
        }

        public async Task<int> CreateBar (BarCreationRequest request)
        {
            int newId = int.MinValue;

            try
            {

                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Client.PostAsync("api/bar", stringContent);

                response.EnsureSuccessStatusCode();

                newId = await response.Content.ReadAsAsync<int>(); 
              
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error: {e.Message}");

                Debugger.Break();
            }

            return newId;
        }
    }
}
