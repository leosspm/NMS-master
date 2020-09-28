using KERRY.NMS.BL.Extensions;
using KERRY.NMS.CORE;
using KERRY.NMS.CORE.Enums;
using KERRY.NMS.CORE.Utilities;
using KERRY.NMS.DAL.Respository;
using KERRY.NMS.MODEL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KERRY.NMS.BL.Service
{
    public interface ISMSService
    {
        Task<bool> SendSMS(SMSDataModel data);
    }


    public class SMSService : ISMSService
    {
        private readonly ISMSRepository smsRepository;
        public SMSService(ISMSRepository _smsRepository)
        {
            this.smsRepository = _smsRepository;
        }

        public async Task<bool> SendSMS(SMSDataModel data)
        {
            var exception = data.MessageDataValidation();

            if (exception == null)
            {
                switch (data.MessageType)
                {
                    case MessageType.StatusUpdate:
                        string messageContent = GetMessageTemplateByStatus(data.OrderInfo);
                        return await smsRepository.StoreSMSToORC(messageContent, data.PhoneNumber.FormatPhoneNumber_vi(), GetRecipient(data.Source));
                    case MessageType.OTP:
                        //OTP must using recipient = 'KENV'
                        return await smsRepository.StoreSMSToORC(data.MessageContent, data.PhoneNumber.FormatPhoneNumber_vi(), GetRecipient(KEVNSource.KEVN));
                    case MessageType.Other:
                        return await smsRepository.StoreSMSToORC(data.MessageContent, data.PhoneNumber.FormatPhoneNumber_vi(), GetRecipient(data.Source));

                    default:
                        return false;
                }
            }
            throw exception;
        }

        private string GetRecipient(KEVNSource source)
        {
            return Enum.GetName(typeof(KEVNSource), source);
        }

        private string GetMessageTemplateByStatus(OrderInfo orderInfo)
        {
            switch (orderInfo.StatusCode)
            {
                case ShipmentStatus.PIN:
                    return string.Format(MessageTemplate.STAFF_ACCEPTED_PICKUP, orderInfo.OrderNumber);
                case ShipmentStatus.CANCEL:
                    return string.Format(MessageTemplate.STAFF_CANCEL_PICKUP, orderInfo.OrderNumber, orderInfo.Reason);
                case ShipmentStatus.PUP:
                    return string.Format(MessageTemplate.STAFF_PICKUP_SUCCESSED, orderInfo.OrderNumber);
                case ShipmentStatus.PUX:
                    return string.Format(MessageTemplate.STAFF_PICKUP_FAILED, orderInfo.OrderNumber, orderInfo.Reason);
                case ShipmentStatus.POD:
                    return string.Format(MessageTemplate.STAFF_DELIVER_SUCCESSED, orderInfo.OrderNumber);

                default:
                    return null;
            }
        }
    }
}
