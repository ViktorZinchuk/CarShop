using CarShopCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMW3series
{
    [Serializable]
    public class Series3 : MarshalByRefObject, ICar
    {
        public string Vendor { get; set; }
        public string Model { get; set; }
        public Color CarColor { get; set; }
        public double Price { get; set; }
        public Series3()
        {
            Vendor = "BMW";
            Model = "3 series";
            CarColor = Color.Brown;
            Price = 50000.00;
        }
    }
}
