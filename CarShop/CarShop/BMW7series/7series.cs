using CarShopCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMW7series
{
    public class Series7 : MarshalByRefObject, ICar
    {
        public string Vendor { get; set; }
        public string Model { get; set; }
        public Color CarColor { get; set; }
        public double Price { get; set; }
        public Series7()
        {
            Vendor = "BMW";
            Model = "7 series";
            CarColor = Color.Black;
            Price = 40000.00;
        }
    }
}
