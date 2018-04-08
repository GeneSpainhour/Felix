using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FelixTests
{
    public class TestUtils
    {
        public static void Report (object item)
        {
            if (item != null)
            {
                List<PropertyInfo> properties = item.GetType().GetProperties().ToList();

                StringBuilder bldr = new StringBuilder();

                properties.ForEach(p => bldr.Append($"{p.Name}: {p.GetValue(item, null)},"));

                bldr.AppendLine();

                Debug.WriteLine(bldr.ToString());
            }
        }
    }
}
