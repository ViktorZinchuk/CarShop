using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarShopCommon
{
    [Serializable]
    public class CommonCar : ICar
    {
        public string Vendor { get; set; }
        public string Model { get; set; }
        public Color CarColor { get; set; }
        public double Price { get; set; }
        public CommonCar(string Vendor,string Model, Color CarColor, double Price)
        {
            this.Vendor = Vendor;
            this.Model = Model;
            this.CarColor = CarColor;
            this.Price = Price;
        }
    }
}
