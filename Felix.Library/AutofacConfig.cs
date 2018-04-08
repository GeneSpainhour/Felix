using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Felix.Library
{
    public class AutofacConfig
    {
        private static bool Registered = true;

        public static void Register()
        {
            if (!Registered)
            {
                ContainerBuilder builder = new ContainerBuilder();


                Registered = true;
            }
        }


    }
}
