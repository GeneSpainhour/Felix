using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FelixConsole
{
    public class ListAddTester
    {
        public static void Test ()
        {
            List<Data> givenList = Data.Values();

            for (int i = 0; i< 10; ++i)
            {
                Data data = Next(givenList);

                givenList.Add(data);

               
            }

            Report(givenList);
        }

        private static Data Next(IEnumerable<Data> given)
        {
            var inverted = given.OrderByDescending(d => d.Pos).Take(2).ToList();

            int value = inverted.Sum(t => t.Value);

            Data newData = new Data(given.Count(), value);

            return newData;
        }

        private static void Report (List<Data> data)
        {
            data.ForEach(d => Console.WriteLine($"Pos: {d.Pos}, Value: {d.Value}"));
        }
    }

    class Data
    {
        public int Pos { get; set; }

        public int Value { get; set; }

        public Data (int pos, int value)
        {
            Pos = pos;

            Value = value;
        }

        public static List<Data> Values()
        {
            List<Data> list = new List<Data>();

            for (int i = 0; i< 2; ++i)
            {
                list.Add(new Data(i, i+1));
            }

            return list;
        }
    }
}
