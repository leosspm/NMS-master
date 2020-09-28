using KERRY.NMS.MODEL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.BL.Extensions
{
    public static class Extension
    {
        public static Exception MessageDataValidation(this SMSDataModel data)
        {
            if (data == null)
            {
                return new ArgumentNullException("data");
            }
            if (string.IsNullOrEmpty(data.PhoneNumber))
            {
                return new ArgumentException("Phone number cannot be null", "PhoneNumber");
            }
            if (data.MessageType != CORE.Enums.MessageType.StatusUpdate && string.IsNullOrEmpty(data.MessageContent))
            {
                return new ArgumentException("Message content cannot be null", "MessageContent");
            }
            if (data.MessageType == CORE.Enums.MessageType.StatusUpdate)
            {
                if(data.OrderInfo == null)
                    return new ArgumentNullException("OrderInfo");
                else
                {
                    if(string.IsNullOrEmpty(data.OrderInfo.OrderNumber))
                        return new ArgumentException("Order number cannot be null", "OrderNumber");
                    if (string.IsNullOrEmpty(data.OrderInfo.StatusCode))
                        return new ArgumentException("Status code cannot be null", "StatusCode");
                }
            }

            return null;
        }

    }
}
