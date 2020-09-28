using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KERRY.NMS.API.TestApiKey.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KERRY.NMS.KEFIREBASEADMIN;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;

namespace KERRY.NMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    [Authorize]
    public class TestApiKeyController : ApiBaseController
    {
        public TestApiKeyController()
        {
        }

        [HttpGet("anyone")]
        public IActionResult Anyone()
        {
            var message = $"Hello from {nameof(Anyone)}";
            return new ObjectResult(message);
        }

        [HttpGet("sendpush")]
        public async void sendpush()
        {
            KEFirebaseAdmin app = new KEFirebaseAdmin("EBK-APP");
            var data = new Dictionary<string, string>();
            data.Add("Title", "test");
            data.Add("Body", "test");

            Notification notification = new Notification()
            {
                Title = "Test push noti",
                Body = "Test"
            };
            await app.sendNotificationByTopic("BOTH",
                "3538",
                data, notification);
        }

        [HttpGet("only-authenticated")]
        [Authorize]
        public IActionResult OnlyAuthenticated()
        {
            var message = $"Hello from {nameof(OnlyAuthenticated)}";
            return new ObjectResult(message);
        }

        [HttpGet("only-employees")]
        [Authorize(Policy = Policies.OnlyEmployees)]
        public IActionResult OnlyEmployees()
        {
            var message = $"Hello from {nameof(OnlyEmployees)}";
            return new ObjectResult(message);
        }

        [HttpGet("only-managers")]
        [Authorize(Policy = Policies.OnlyManagers)]
        public IActionResult OnlyManagers()
        {
            var message = $"Hello from {nameof(OnlyManagers)}";
            return new ObjectResult(message);
        }

        [HttpGet("only-third-parties")]
        [Authorize(Policy = Policies.OnlyThirdParties)]
        public IActionResult OnlyThirdParties()
        {
            var message = $"Hello from {nameof(OnlyThirdParties)}";
            return new ObjectResult(message);
        }
    }
}