using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.CORE.Utilities
{
    public static class NotificationType
    {
        public static string NOTIFICATION = "NOTIFICATION";
        public static string DATA = "DATA";
        public static string BOTH = "BOTH";
    }


    public static class NotificationSendType
    {
        public static string SINGLE_DEVICE = "SINGLE_DEVICE";
        public static string MULTIPLE_DEVICE = "MULTIPLE_DEVICE";
        public static string TOPIC = "TOPIC";
        public static string CONDITION = "CONDITION";
    }


    public static class NotificationDeviceAction
    {
        public static string WEB = "WEB";
    }

    public static class NotificationResultCode
    {
        public static int SUCCESS = 4200;
        public static int FAILURE = 4100;
    }

    public static class KEFirebaseErrorMessage
    {
        public const string NO_APP = "Can't find the app specified";
        public const string WRONG_NOTIFICATION_TYPE_MODE = "Please specify correct notification type: DATA, NOTIFICATION, BOTH";
        public const string EMPTY_TOPIC = "Topic can't be empty";
        public const string EMPTY_TOKENS = "Tokens list can't be empty";
    }
}
