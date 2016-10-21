using CarShopCommon;
using CarShopWebSite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarShop
{
    class FileWatcher
    {
        private Dictionary<string,string> Dictionary;
        string WcfUri = System.Configuration.ConfigurationSettings.AppSettings["WCFUri"];
        public FileWatcher()
        {
            Dictionary = new Dictionary<string,string>();
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Filter = "*.dll";

            watcher.Created += new FileSystemEventHandler(AddCar);
            watcher.Deleted += new FileSystemEventHandler(DeleteCar);

            watcher.Path = System.Configuration.ConfigurationSettings.AppSettings["WatchPath"];

            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = false;
            watcher.NotifyFilter = NotifyFilters.FileName;
        }
        public void StartWork()
        {
                string[] dirs = Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["WatchPath"], "*.dll");
                AppDomain appDomain = AppDomain.CreateDomain("New Domain");
                foreach (string dir in dirs)
                {
                     Type type = typeof(Proxy);
                     var value = (Proxy)appDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);

                     ICar AddEdCar = (ICar)value.GetAssembly(dir);
                     Dictionary.Add(dir, AddEdCar.Model);

                     AddCar(AddEdCar);
                }
                AppDomain.Unload(appDomain);
        }

        private void DeleteCar(object sender, FileSystemEventArgs e)
        {
            DeleteCar(Dictionary[e.FullPath]);
            Dictionary.Remove(e.FullPath);
        }

        private void AddCar(object sender, FileSystemEventArgs e)
        {
            AppDomain appDomain = AppDomain.CreateDomain("New Domain");
            Type type = typeof(Proxy);
            var value = (Proxy)appDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);

            ICar AddEdCar = (ICar)value.GetAssembly(e.FullPath);
            Dictionary.Add(e.FullPath, AddEdCar.Model);

            AddCar(AddEdCar);
            AppDomain.Unload(appDomain);
       
        }
        public void AddCar(ICar car)
        {
            using (ChannelFactory<IWebSiteService> scf = new ChannelFactory<IWebSiteService>(new BasicHttpBinding(), WcfUri))
            {
                IWebSiteService channel = scf.CreateChannel();
                CommonCar addedcar = new CommonCar(car.Vendor, car.Model, car.CarColor, car.Price);
                channel.AddCar(addedcar);
            }
        }
        public void DeleteCar(String CarName)
        {
            using (ChannelFactory<IWebSiteService> scf = new ChannelFactory<IWebSiteService>(new BasicHttpBinding(), WcfUri))
            {
                IWebSiteService channel = scf.CreateChannel();
                CommonCar car = new CommonCar(System.Configuration.ConfigurationSettings.AppSettings["VendorName"], CarName, Color.Black, 0);
                channel.DeleteCar(car);
            }
        }
    }
}
