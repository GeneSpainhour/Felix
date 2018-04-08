using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelixConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // ListAddTester.Test();
            //DateTimeTester.Test();
            //LamdaTester.Test();
            //ApiStartTest.Test();

            AsyncTester.Test();

            Console.WriteLine("Press any key");

            Console.ReadKey();
            
        }
    }
}
