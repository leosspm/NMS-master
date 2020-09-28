﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KERRY.NMS.API.TestApiKey.Authorization
{
    public class OnlyEmployeesAuthorizationHandler : AuthorizationHandler<OnlyEmployeesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyEmployeesRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Employee))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
