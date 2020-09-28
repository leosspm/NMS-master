using Microsoft.AspNetCore.Authorization;

namespace KERRY.NMS.API.TestApiKey.Authorization
{
    public class OnlyManagersRequirement : IAuthorizationRequirement
    {
    }
}
