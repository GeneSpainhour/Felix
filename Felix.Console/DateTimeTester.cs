using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelixConsole
{
    public class DateTimeTester
    {
        public static void Test ()
        {
            int year = 2018;

            int month = 3;

            for (int i =1; i<=31; ++i)
            {
                DateTime dt = new DateTime(year, month, i);

                Console.WriteLine($"{dt.ToString()}");
            }
        }
    }
}
