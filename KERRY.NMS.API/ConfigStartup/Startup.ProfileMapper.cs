using KERRY.NMS.BL.Service;
using KERRY.NMS.DAL.Respository;
using Microsoft.Extensions.DependencyInjection;

namespace KERRY.NMS.API
{
    public partial class Startup
    {
        private void ConfigProfileMapper(IServiceCollection services)
        {
            //// UNITY
            //var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            //GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //UnityContainerHolder.Container = UnityConfig.GetConfiguredContainer();
            services.AddScoped<ISMSRepository, SMSRepository>();
            services.AddTransient<ISMSService, SMSService>();
        }
    }
}
