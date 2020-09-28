using System;
using System.Threading.Tasks;
using KERRY.NMS.API.TestApiKey.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KERRY.NMS.KEFIREBASEADMIN;
using KERRY.NMS.MODEL.Model;
using KERRY.NMS.CORE;

namespace KERRY.NMS.API.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class FirebaseController : ApiBaseController
    {
        public FirebaseController()
        {
        }

        [HttpPost]
        [Route("SendPushByTopic")]
        public async Task<IActionResult> SendPushByTopic(FirebasePayload payload)
        {
            try
            {
                KEFirebaseAdmin app = new KEFirebaseAdmin(payload.AppName);
                String response;
                try
                {
                    response = await app.sendNotificationByTopic(
                        NotificationTypeMode: payload.NotificationTypeMode,
                        topic: payload.Topic,
                        data: payload.Data,
                        notification: payload.Notification,
                        androidIcon: payload.AndroidIcon,
                        androidColor: payload.AndroidColor,
                        apnsBadge: payload.APNSBadge);
                } 
                catch(Exception e)
                {
                    return await ResponseData(false, e.Message, ApiResponseMessage.ErrorMessage);
                };
                return await ResponseData(true, response, ApiResponseMessage.SuccessMessage);
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return await ResponseData(false, msg, ApiResponseMessage.ErrorMessage);
            }
        }
        [HttpPost]
        [Route("SendPushByTokens")]
        public async Task<IActionResult> SendPushByTokens(FirebasePayload payload)
        {
            try
            {
                KEFirebaseAdmin app = new KEFirebaseAdmin(payload.AppName);
                String response = await app.sendNotificationByTokens(
                    NotificationTypeMode: payload.NotificationTypeMode,
                    topic: payload.Topic,
                    Tokens: payload.Tokens,
                    data: payload.Data,
                    notification: payload.Notification,
                    androidIcon: payload.AndroidIcon,
                    androidColor: payload.AndroidColor,
                    apnsBadge: payload.APNSBadge
                    );
                return new ObjectResult(response);
            }
            catch (Exception e) 
            {
                string msg = e.Message;
                return new ObjectResult(msg);
            }
        }
    }
}
