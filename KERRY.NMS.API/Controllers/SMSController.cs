using KERRY.NMS.BL.Service;
using KERRY.NMS.CORE;
using KERRY.NMS.CORE.Enums;
using KERRY.NMS.MODEL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace KERRY.NMS.API.Controllers
{
    [ApiController]
    public class SMSController : ApiBaseController
    {
        private readonly ISMSService smsService;
        public SMSController(ISMSService _sMSService)
        {
            this.smsService = _sMSService;
        }

        [HttpPost]
        [Route("SendSMS")]
        public async Task<IActionResult> SendSMS(SMSDataModel data)
        {
            try
            {                
                var userId = GetDataFromClaim(ApiClaimTypes.UserId);
                var result = await smsService.SendSMS(data);
                return await ResponseData(true, result, ApiResponseMessage.SuccessMessage);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message.ToString());
                return await ResponseData(false, ex.Message, ApiResponseMessage.ErrorMessage);
            }
        }

        //[HttpPost]
        //[Route("SendStatusUpdateSMS")]
        //public async Task<IActionResult> SendStatusUpdateSMS(SMSDataModel data)
        //{
        //    try
        //    {                
        //        var userId = GetDataFromClaim(ApiClaimTypes.UserId);
        //        var result = await smsService.StoreSMSToORC(data.MessageContent, data.PhoneNumber, RecipientType.EBK);

        //        return await ResponseData(true, result, ApiResponseMessage.SuccessMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        return await ResponseData(false, ex.ToString(), ApiResponseMessage.ErrorMessage);

        //    }
        //}
    }
}