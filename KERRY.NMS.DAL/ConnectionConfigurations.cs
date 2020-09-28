using System;
using Microsoft.Extensions.Configuration;

namespace KERRY.NMS.DAL
{
    public static class ConnectionConfigurations
    {
        private static string _sqlServerConnectionString;
        public static IConfigurationRoot Configuration { get; set; }

        public static string ConnectionStringSQL
        {
            get
            {
                bool flag = string.IsNullOrEmpty(_sqlServerConnectionString);
                if (flag)
                {
                    //_sqlServerConnectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                }
                return _sqlServerConnectionString;
            }
        }

        private static string _oracleConnectionString;
        public static string ConnectStringOracle
        {
            get
            {
                if (string.IsNullOrEmpty(_oracleConnectionString))
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                    Configuration = builder.Build();
                    _oracleConnectionString = Configuration["ConnectionStrings:OracleConnectionString"];
                }
                return _oracleConnectionString;
            }
        }

    }
}
