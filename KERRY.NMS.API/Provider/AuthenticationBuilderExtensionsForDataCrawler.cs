using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KERRY.NMS.API.Provider
{
    public static class AuthenticationBuilderExtensionsForDataCrawler
    {
        public static AuthenticationBuilder AddApiKeySupportForDataCrawler(this AuthenticationBuilder authenticationBuilder, Action<ApiCrawlDataProviderOptions> options)
        {
            return authenticationBuilder.AddScheme<ApiCrawlDataProviderOptions, ApiCrawlDataProviderMiddleware>(ApiCrawlDataProviderOptions.DefaultScheme, options);
        }
    }
}
