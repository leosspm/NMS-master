using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using FirebaseAdmin.Messaging;

namespace KERRY.NMS.MODEL.Model
{
    public class FirebasePayload
    {
        public List<String> Tokens { get; set; }
        public KEFirebaseData Data { get; set; }
        public Notification Notification { get; set; }
        public  string AppName { get; set; }
        public string NotificationTypeMode { get; set; }
        public string Topic { get; set; }

        public string AndroidIcon { get; set; }
        public string AndroidColor { get; set; }
        public int APNSBadge { get; set; }
    }
}
