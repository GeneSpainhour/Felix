using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;

namespace FelixTests
{
    public class CDSWriter
    {
        public static void Write (string fileSpec, IEnumerable<(IBar, IInstaTrendValue)> values)
        {
            List<PropertyInfo> barProperties = typeof (IBar).GetProperties().ToList();

            List<PropertyInfo> valueProperties = typeof (IInstaTrendValue).GetProperties().ToList();

            List<(IBar, IInstaTrendValue)> valueList = values.ToList();

            List<string> barFields = new List<string> { "Time", "Open", "High", "Low", "Close" };

            List<string> valueFields = new List<string> { "Value", "DValue", "D2Value"};

            using (StreamWriter writer = File.CreateText(fileSpec))
            {
                StringBuilder bldr = new StringBuilder();

                barFields.ForEach(f => bldr.Append($"{f}, "));

                int i = 0, c = valueFields.Count;

                valueFields.ForEach(f => bldr.AppendFormat("{0}{1}", f, c - i++ > 1 ? ", " : ""));

                bldr.AppendLine();

                writer.Write(bldr.ToString());

                bldr.Clear();

                foreach (var item in valueList)
                {
                    GetValues(item.Item1, barProperties, barFields, bldr);

                    GetValues(item.Item2, valueProperties, valueFields, bldr);

                    bldr.AppendLine();

                    writer.Write(bldr.ToString());

                    bldr.Clear();
                }
            }
        }

        private static void GetValues(IBar bar, List<PropertyInfo> properties, List<string> fields, StringBuilder bldr)
        {
            foreach ( string field in fields)
            {
                var pInfo = properties.First(p => p.Name.Equals(field));

                bldr.Append($"{pInfo.GetValue(bar, null)},");
            }
        }

        private static void GetValues(IInstaTrendValue bar, List<PropertyInfo> properties, List<string> fields, StringBuilder bldr)
        {
            var propertiesToWrite = properties.Where(p => fields.Contains(p.Name)).ToList();

            int i = 0, fieldCount = fields.Count;

            foreach( var field in fields)
            {
                var pInfo = properties.First(p => p.Name.Equals(field));

                bldr.AppendFormat("{0}{1}", pInfo.GetValue(bar, null), fieldCount - i++ > 1 ? ", " : "");

            }
        }
    }
}
