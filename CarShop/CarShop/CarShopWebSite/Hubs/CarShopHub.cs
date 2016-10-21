using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CarShopWebSite.Hubs
{
    public class CarShopHub : Hub
    {
        public void AddCar(string vendor,string  model)
        {
            Clients.All.AddCar(vendor, model);
        }
        public void DeleteCar(string vendor, string model)
        {
            Clients.All.DeleteCar(vendor, model);
        }
        public void AddVendor(string vendor)
        {
            Clients.All.AddVendor(vendor);
        }
        public void DeleteVendor(string vendor)
        {
            Clients.All.DeleteVendor(vendor);
        }
        public void BuyCar(string model)
        {
            Clients.All.BuyCar(model);
        }
    }
}