using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KERRY.NMS.API.Provider
{
    public class TokenProviderOptions
    {
        public string Path { get; set; }
        public TimeSpan Expiration { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
