using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VendorClient
{
    public class OrdersDAL
    {
        string path = System.Configuration.ConfigurationSettings.AppSettings["VendorOrders"];
        public void AddOrder(string Model)
        {
            XDocument xDocument = XDocument.Load(path);

            XElement orders =  xDocument.Descendants("Orders").First();
            var element = new XElement("Order");
            element.Value = Model;
            orders.Add(element);

            xDocument.Save(path);
        }
    }
}
