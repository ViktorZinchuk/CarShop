using CarShopCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace CarShop
{
    public class Proxy : MarshalByRefObject
    {
        public object GetAssembly(string assemblyPath)
        {
            Assembly assembly = Assembly.LoadFile(assemblyPath);
            var type = typeof(ICar);
            var carType = assembly.GetTypes().Where(p => type.IsAssignableFrom(p)).First();
            object carInstance = Activator.CreateInstance(carType);
            return carInstance;
        }
    }
}
