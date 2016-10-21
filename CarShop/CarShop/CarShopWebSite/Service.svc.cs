using CarShopWebSite.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CarShopWebSite
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WebSiteService : IWebSiteService
    {
        CarsDAL Dal = new CarsDAL();
        IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<CarShopHub>();
        public void AddCar(CarShopCommon.CommonCar car)
        {
            Dal.AddCar(car.Vendor, car.Model, car.CarColor, car.Price);
            hubContext.Clients.All.AddCar(car.Vendor, car.Model);
        }

        public void DeleteCar(CarShopCommon.CommonCar car)
        {
            Dal.RemoveCar(car.Vendor, car.Model);
            hubContext.Clients.All.DeleteCar(car.Vendor, car.Model);
        }
    }
}
