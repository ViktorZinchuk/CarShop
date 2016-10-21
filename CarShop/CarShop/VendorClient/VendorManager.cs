using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace VendorClient
{
    class VendorManager
    {
        string VendorName = System.Configuration.ConfigurationSettings.AppSettings["VendorName"];
        public string ServiceUri = System.Configuration.ConfigurationSettings.AppSettings["SiteURI"];
        public string WcfServiceUri;
        public string Registration()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = System.Text.UTF8Encoding.UTF8;
                string dwml;
                dwml = webClient.DownloadString(ServiceUri + "Url?VendorName=" + VendorName + "&ServiceUrl=" + System.Configuration.ConfigurationSettings.AppSettings["ServiceUrl"]);

                WcfServiceUri = JsonConvert.DeserializeObjectAsync<string>(dwml).Result;
            }
            ChangeAppConfig();
            StartService("Vendor Windows Service");

            return WcfServiceUri;
        }


        public void UnRegistration()
        {
            StopService("Vendor Windows Service");
            using (WebClient wc = new WebClient())
            {
                MemoryStream ms = new MemoryStream();
                wc.UploadData(ServiceUri + VendorName, "DELETE", ms.ToArray());
            }
        }
        public void StartService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                service.Start();
            }
            catch
            {
                // ...
            }

        }
        public void StopService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                service.Stop();
            }
            catch
            {
                // ...
            }
        }

        public void ChangeAppConfig()
        {
            string exePath = @"D:\Carshop\ws\CarShop.exe";
            System.Configuration.Configuration config =
            ConfigurationManager.OpenExeConfiguration(exePath);
            config.AppSettings.Settings["WCFUri"].Value = WcfServiceUri;
            config.Save();
        }
    }
}
