using CarShopWebSite.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarShopWebSite.Controllers
{
    public class VendorsController : ApiController
    {
        CarsDAL Dal = new CarsDAL();
        IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<CarShopHub>();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string GetUrl([FromUri] string VendorName,[FromUri] string ServiceUrl)
        {
            bool existvendor = Dal.SetVendor(VendorName, ServiceUrl);
            if(!existvendor)
            {
                hubContext.Clients.All.AddVendor(VendorName);
            }
            return System.Configuration.ConfigurationSettings.AppSettings["ServiceUrl"];
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
            Dal.DeleteVendor(id);
            hubContext.Clients.All.DeleteVendor(id);
        }
    }
}