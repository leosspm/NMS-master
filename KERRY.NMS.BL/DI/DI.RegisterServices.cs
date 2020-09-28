using KERRY.NMS.BL.Service;
using KERRY.NMS.CORE.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace KERRY.NMS.BL.DI
{
    public class RegisterServices : IUnityBootstrapper
        {
            public void Register(IUnityContainer container)
            {
                container.RegisterType<ISMSService, SMSService>();
            }
        }
    }
