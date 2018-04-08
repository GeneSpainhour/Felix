using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Messaging
{
    public class ConsoleReporter
    {
        protected void Report(string msg)
        {
            Console.WriteLine(msg);

            Debug.WriteLine(msg);
        }
    }
}
