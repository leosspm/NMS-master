using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.CORE
{
    public class ApiClaimTypes
    {
        public string ClaimType { get; private set; }

        private ApiClaimTypes(string claimType)
        {
            ClaimType = claimType;
        }

        public static ApiClaimTypes UserId => new ApiClaimTypes("UserId");
        public static ApiClaimTypes UserName => new ApiClaimTypes("UserName");
        public static ApiClaimTypes Contact => new ApiClaimTypes("Contact");
        public static ApiClaimTypes Role => new ApiClaimTypes("Role");
    }
}
