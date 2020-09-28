using KERRY.NMS.API.Provider;
//using KERRY.NMS.API.TestApiKey.Authentication;
//using KERRY.NMS.API.TestApiKey.Authorization;
using KERRY.NMS.CORE;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using KERRY.NMS.API.Provider.Policies;
namespace KERRY.NMS.API
{
    public partial class Startup
    {
        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            string tokenKey = Configuration.GetSection("ApplicationKeys")["TokenKey"];
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        }

        private void ConfigSecurity(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(Convert.ToInt32(Configuration.GetSection("ApplicationKeys")["ExpiredDays"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetSymmetricSecurityKey()
                };
                x.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            })
            //.AddApiKeySupport(options => {});
            .AddApiKeySupportForDataCrawler(options => {
                options.ApiKeyCrawlData = Configuration.GetSection("ApplicationKeys")["ApiKeyCrawlData"];
                options.ApiRoleCrawlData = Configuration.GetSection("ApplicationKeys")["ApiRoleCrawlData"];
            });

            services.AddAuthorization(x =>
            {
                x.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
                //x.AddPolicy("RoleAdmin", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.Admin));
                //x.AddPolicy("RoleInputter", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.Inputter));
                //x.AddPolicy("RoleChecker", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.Checker));
                //x.AddPolicy("RoleSale", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.Sale));
                //x.AddPolicy("RoleAgent", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.Agent));
                //x.AddPolicy("RoleSaleManager", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.SaleManager));
                //x.AddPolicy("RoleDataAdmin", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.DataAdmin));
                //x.AddPolicy("RoleInvestor", policy => policy.RequireClaim(ApiClaimTypes.Role.ClaimType, ApiConstantRole.Investor));

                x.AddPolicy(Policies.ApiDataCrawler, policy =>
                {
                    policy.AuthenticationSchemes.Add(ApiCrawlDataProviderOptions.DefaultScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            //services.AddSingleton<IAuthorizationHandler, OnlyManagersAuthorizationHandler>();
            //services.AddSingleton<IGetApiKeyQuery, InMemoryGetApiKeyQuery>();
        }

        private void ConfigSecurity(IApplicationBuilder app, IWebHostEnvironment envs)
        {
            app.UseCors(x =>
                x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

            app.UseMiddleware<TokenProviderMiddleware>(new TokenProviderOptions()
            {
                Path = Configuration.GetSection("ApplicationKeys")["PathLogin"],
                Expiration = TimeSpan.FromDays(Convert.ToInt32(Configuration.GetSection("ApplicationKeys")["ExpiredDays"])),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            });

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
