using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KERRY.NMS.CORE;
using KERRY.NMS.MODEL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using KERRY.NMS.KEFIREBASEADMIN;

namespace KERRY.NMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
        protected readonly ILogger logger = LogManager.GetCurrentClassLogger();

        protected async Task<OkObjectResult> ResponseData(bool isSuccess, dynamic dataResponse, ApiResponseMessage message)
        {
            ResponseViewModel response = new ResponseViewModel()
            {
                IsSuccess = isSuccess,
                Message = message.Message,
                Data = dataResponse
            };
            return await Task.FromResult(Ok(response.ToJsonCamel()));
        }

        protected string GetDataFromClaim(ApiClaimTypes claim)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.Claims.Any(c => c.Type == claim.ClaimType))
            {
                return identity.Claims.FirstOrDefault(c => c.Type == claim.ClaimType).Value;
            }
            return "";
        }
    }
}