using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KERRY.NMS.API.TestApiKey.Authorization
{
    public class OnlyManagersAuthorizationHandler : AuthorizationHandler<OnlyManagersRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyManagersRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Manager))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
