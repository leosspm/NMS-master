using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace KERRY.NMS.CORE.Unity
{
    public interface IUnityBootstrapper
    {
        void Register(IUnityContainer container);
    }
}
