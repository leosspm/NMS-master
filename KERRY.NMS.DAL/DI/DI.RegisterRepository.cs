using KERRY.NMS.CORE.Unity;
using KERRY.NMS.DAL.Respository;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace KERRY.NMS.DAL.DI
{
    public class DI_RegisterRepository : IUnityBootstrapper
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<ISMSRepository, SMSRepository>();
        }
    }
}
