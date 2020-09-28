using KERRY.NMS.BL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace KERRY.NMS.API
{
    public partial class Startup
    {
        private void ConfigBussinessLayer(IServiceCollection services)
        {
            Uri baseUrl = new Uri(Convert.ToString(Configuration.GetSection("ApplicationKeys")["ApiCrawlData"]));
            string connectionString = Configuration.GetConnectionString("ConnectionString");

            NMSRegisterService.nmsRegisterService(services, baseUrl, connectionString);
        }
    }
}
