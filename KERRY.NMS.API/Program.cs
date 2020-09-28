using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using KERRY.NMS.KEFIREBASEADMIN;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using System.Collections.Generic;

namespace KERRY.NMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseUrls("http://localhost:9500");
                });
    }
}
