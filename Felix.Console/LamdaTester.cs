using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelixConsole
{
    public class LamdaTester
    {
        public static void Test ()
        {
            //var docList = Doc.Docs.ToList();

            //Display(docList, d => d.Name);

            //foreach ( Doc doc in docList)
            //{
            //    Display(doc, d=> d.Name);
            //}
            var vectorList = Vector.Vectors.ToList();

            Display(vectorList, v => Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2)));
        }

        private static void Display(IEnumerable<Vector> vectors, Func<Vector, object> func)
        {
            vectors.ToList().ForEach(v => Console.WriteLine($"Mag({v.X},{v.Y}):{func(v)}"));
        }

        private static void Display(IEnumerable<Doc> docList, Func<Doc, object> member)
        {
            docList.ToList().ForEach(d => Console.WriteLine($"{member(d)}"));
        }

        private static void Display(Doc d,  Func<Doc,object> item)
        {
            Console.WriteLine($"{item(d)}");
        }
    }

    public class Vector
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Vector(double x, double y)
        {
            X = x;

            Y = y;
        }

        public static IEnumerable<Vector> Vectors
        {
            get
            {
                return new List<Vector>
                {
                    new Vector(1.0, 2.0),
                    new Vector(3.0, 2.0),
                    new Vector(3.0, 3.0),
                    new Vector(4.0, 5.0),
                    new Vector(5.0, 8.0)

                };
            }
        }

    }

    public class Doc
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Doc(int id, string name)
        {
            Id = id;

            Name = name;
        }

        public static IEnumerable<Doc> Docs
        {
            get
            {
                return new List<Doc>
                {
                    new Doc (1, "abe"),
                    new Doc (2, "ben"),
                    new Doc (3, "cal"),
                    new Doc (4, "deb"),
                    new Doc (5, "eve")

                };
            }
        }

    }
}
