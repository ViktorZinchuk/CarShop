using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using VendorWcfService;

namespace CarShopWebSite.Controllers
{
    public class HomeController : Controller
    {
        CarsDAL DAL = new CarsDAL();
        public ActionResult Index()
        {
            List<string> vendors = DAL.SelectVendors();
            if (vendors.Count != 0)
            {
                List<string> Models = DAL.SelectModels(vendors[0]);
                ViewBag.Vendors = vendors;
            }
            
            return View();
        }
        [HttpGet]
        public JsonResult SelectModles(string vendorname)
        {
                List<string> Models = DAL.SelectModels(vendorname);

                return Json(new { Models }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ModelInfo(string model)
        {
            List<string> Models = DAL.SelectCarInfo(model);

            return Json(new { Models }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BuyCar(string model)
        {
            DAL.BuyCar(model);
            return null;
        }


        [HttpGet]
        public ActionResult MakeOrder(string model, string vendorName)
        {
            string result;
            using (ChannelFactory<IVendorService> scf = new ChannelFactory<IVendorService>(new BasicHttpBinding(), DAL.SelectVendorServiceUrl(vendorName)))
            {
                IVendorService channel = scf.CreateChannel();
                result = channel.MakeOrder(model);
            }

            return Content(result);
        }
    }
}
