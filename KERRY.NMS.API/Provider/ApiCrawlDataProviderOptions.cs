using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KERRY.NMS.API.Provider
{
    public class ApiCrawlDataProviderOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "API Key";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;

        public string ApiKeyCrawlData { get; set; }
        public string ApiRoleCrawlData { get; set; }
    }
}
