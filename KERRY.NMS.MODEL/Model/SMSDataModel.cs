using KERRY.NMS.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.MODEL.Model
{
    public class SMSDataModel
    {
        public string PhoneNumber { get; set; }
        public string MessageContent { get; set; }
        public MessageType MessageType { get; set; }
        public KEVNSource Source { get; set; }
        public OrderInfo OrderInfo { get; set; }
    }
}
