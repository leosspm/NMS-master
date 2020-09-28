using KERRY.NMS.BL;
using KERRY.NMS.CORE;
using KERRY.NMS.MODEL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KERRY.NMS.API.Provider
{
    public class TokenProviderMiddleware
    {
        protected readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate next;
        private readonly TokenProviderOptions options;

        public TokenProviderMiddleware(RequestDelegate next, TokenProviderOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                if (!httpContext.Request.Path.Equals(options.Path, StringComparison.Ordinal))
                {
                    return next(httpContext);
                }

                if (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType)
                {
                    httpContext.Response.StatusCode = 400;
                    return httpContext.Response.WriteAsync(ResponseData(false, null, ApiResponseMessage.BadRequestMessage).Result);
                }

                return GenerateToken(httpContext);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                httpContext.Response.StatusCode = 500;
                return httpContext.Response.WriteAsync(ResponseData(false, null, ApiResponseMessage.ExceptionMessage).Result);
            }
        }

        private async Task GenerateToken(HttpContext httpContext)
        {
            string username = httpContext.Request.Form["username"];
            string password = httpContext.Request.Form["password"];

            var identity = await GetIdentity(httpContext, username, password);

            if (identity == null)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync(await ResponseData(false, null, ApiResponseMessage.InValidLoginMessage));
                return;
            }

            DateTime currentDay = DateTime.UtcNow;
            DateTime expiredDay = currentDay.Add(options.Expiration);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                NotBefore = currentDay,
                Expires = expiredDay,
                SigningCredentials = options.SigningCredentials
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            LoginViewModel response = new LoginViewModel()
            {
                AccessToken = tokenHandler.WriteToken(token),
                TokenType = "Bearer",
                StartedTime = currentDay.ToString("yyyy/MM/dd hh:mm"),
                ExpiredTime = expiredDay.ToString("yyyy/MM/dd hh:mm"),
            };

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(await ResponseData(true, response, ApiResponseMessage.SuccessMessage));
        }

        private async Task<ClaimsIdentity> GetIdentity(HttpContext httpContext, string userName, string password)
        {
            IUserService serviceUser = httpContext.RequestServices.GetService<IUserService>();
            var user = await serviceUser.UserLogin(userName, password);

            if (user == null)
            {
                return null;
            }

            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ApiClaimTypes.UserId.ClaimType, user.Id.ToString()));
            claims.Add(new Claim(ApiClaimTypes.UserName.ClaimType, user.UserName));


            var listRoles = await serviceUser.GetUserRoleByUserId(user.Id);
            claims.Add(new Claim(ApiClaimTypes.Role.ClaimType, String.Join(",", listRoles)));

            return new ClaimsIdentity(claims);
        }

        private Task<string> ResponseData(bool isSuccess, dynamic dataResponse, ApiResponseMessage message)
        {
            ResponseViewModel response = new ResponseViewModel()
            {
                IsSuccess = isSuccess,
                Message = message.Message,
                Data = dataResponse
            };
            return Task.FromResult(response.ToJsonCamel());
        }
    }
}
