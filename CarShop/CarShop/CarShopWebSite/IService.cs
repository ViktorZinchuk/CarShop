using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CarShopCommon;

namespace CarShopWebSite
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWebSiteService
    {
        [OperationContract]
        void AddCar(CommonCar car);
        [OperationContract]
        void DeleteCar(CommonCar car);
    }
}
