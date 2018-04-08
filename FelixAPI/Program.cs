using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace FelixAPI
{
    class Program
    {
        private static readonly string BaseAddress = "BaseAddress";
        static void Main(string[] args)
        {
            string baseAddress = ConfigurationManager.AppSettings[BaseAddress];

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"FelixAPI running on {baseAddress}\nPress any key to quit");

                Console.ReadLine();

                Console.WriteLine($"FelixAPI shutting down");
            }
        }
    }
}
