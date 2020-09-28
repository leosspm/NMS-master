using KERRY.NMS.BL.ExternalApi;
using KERRY.NMS.BL.Service;
using KERRY.NMS.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KERRY.NMS.BL.DI
{
    public class NMSRegisterService
    {
        private static NMSRegisterService registerService = null;

        public static NMSRegisterService nmsRegisterService(IServiceCollection services, Uri baseUrl, string connectionString)
        {
            if (registerService == null)
            {
                registerService = new NMSRegisterService(services, baseUrl, connectionString);
            }
            return registerService;
        }

        private NMSRegisterService() { }
        private NMSRegisterService(IServiceCollection services, Uri baseUrl, string connectionString)
        {
            ConfigExternalApi(services, baseUrl);

            ConfigBussinessLayer(services, connectionString);
        }

        private void ConfigExternalApi(IServiceCollection services, Uri baseUrl)
        {
            services.AddHttpClient<IApiProduct, ApiProduct>(x => x.BaseAddress = baseUrl);
        }

        private void ConfigBussinessLayer(IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDalService, DalService>(service => new DalService(connectionString));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISMSService, SMSService>();
        }
    }
}
