using CarShopWebSite.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CarShopWebSite
{
    public class CarsDAL
    {
        string path;
        public CarsDAL ()
	    {
            path = HttpContext.Current.Server.MapPath(@"~/App_Data/Cars.xml");  
	    }
        public bool SetVendor(string vendor_name, string ServiceUrl)
        {
            XDocument xDocument = XDocument.Load(path);
            bool Existsvendor = false;
            foreach (var profileElement in xDocument.Descendants("Vendors").ToList())
            {
                
                foreach (var Element in xDocument.Descendants("Vendor").ToList())
                {
                    if (Element.Attribute("VendorName").Value == vendor_name)
                    {
                        Existsvendor = true;
                        break;
                    }
                }

                if (!Existsvendor)
                {
                    var element = new XElement("Vendor");
                    element.SetAttributeValue("VendorName", vendor_name);
                    element.SetAttributeValue("ServiceUrl", ServiceUrl);
                    profileElement.Add(element);
                }
            }
            xDocument.Save(path);
            return Existsvendor;
        }

        public void DeleteVendor(string vendor_name)
        {
            XDocument xDocument = XDocument.Load(path);
            foreach (var profileElement in xDocument.Descendants("Vendor").ToList())
            {
                if (profileElement.Attribute("VendorName").Value == vendor_name)
                {
                    profileElement.Remove();
                }
            }
            xDocument.Save(path);
        }
        public void AddCar(string vendor_name,string model, Color color, double price)
        {
            XDocument xDocument = XDocument.Load(path);
            foreach (var profileElement in xDocument.Descendants("Vendor").ToList())
            {
                if (profileElement.Attribute("VendorName").Value == vendor_name)
                {
                    bool ExistsModel = false;
                    foreach (var Element in xDocument.Descendants("Model").ToList())
                    {
                        if (Element.Attribute("Name").Value == model)
                        {
                            ExistsModel = true;
                            break;
                        }
                    }

                    if (!ExistsModel)
                    {
                        var newElement = new XElement("Model",
                              new XElement("Color", ColorTranslator.ToHtml(color)),
                              new XElement("Price", price),
                              new XElement("State", "Available"));
                        newElement.SetAttributeValue("Name", model);
                        profileElement.Add(newElement);
                    }
                }
            }
            xDocument.Save(path);
        }
        public void RemoveCar(string vendor_name,string model)
        {
            XDocument xDocument = XDocument.Load(path);
            foreach (var profileElement in xDocument.Descendants("Vendor").ToList())
            {
                if (profileElement.Attribute("VendorName").Value == vendor_name)
                {
                    foreach (var el in profileElement.Descendants("Model").ToList())
                    {
                        if (el.Attribute("Name").Value == model)
                        {
                            el.Remove();
                        }
                    }
                }
            }
            xDocument.Save(path);
        }
        public List<string> SelectVendors()
        {
            List<string> Vendors = new List<string>();
            XDocument xDocument = XDocument.Load(path);
            foreach (var profileElement in xDocument.Descendants("Vendor").ToList())
            {
                Vendors.Add(profileElement.Attribute("VendorName").Value);
            }
            return Vendors;
        }
        public List<string> SelectModels(string vendor)
        {
            List<string> Models = new List<string>();
            XDocument xDocument = XDocument.Load(path);
            var vendorElem = xDocument.Descendants("Vendor").Where(x => x.Attribute("VendorName").Value == vendor);
            foreach (var profileElement in vendorElem.Descendants("Model").ToList())
            {
                Models.Add(profileElement.Attribute("Name").Value);
            }
            return Models;
        }
        public List<string> SelectCarInfo(string Model)
        {
            List<string> Models = new List<string>();
            XDocument xDocument = XDocument.Load(path);

            var vendorElem = xDocument.Descendants("Model").Where(x => x.Attribute("Name").Value == Model);
            foreach (var profileElement in vendorElem.Descendants().ToList())
            {
                Models.Add(profileElement.Value);
            }
            return Models;
        }
        public void BuyCar(string Model)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<CarShopHub>();
            XDocument xDocument = XDocument.Load(path);

            var vendorElem = xDocument.Descendants("Model").Where(x => x.Attribute("Name").Value == Model).First();
            foreach (var profileElement in vendorElem.Descendants().ToList())
            {
                if(profileElement.Name=="State")
                {
                    profileElement.Value = "Sold";
                    hubContext.Clients.All.BuyCar(Model);
                    xDocument.Save(path);
                }
            }
        }
        public string SelectVendorServiceUrl(string VendorName)
        {
            XDocument xDocument = XDocument.Load(path);

            var vendorElem = xDocument.Descendants("Vendor").Where(x => x.Attribute("VendorName").Value == VendorName).First();
            return vendorElem.Attribute("ServiceUrl").Value;
        }
    }
}