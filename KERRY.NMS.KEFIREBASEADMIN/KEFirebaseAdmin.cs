using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KERRY.NMS.CORE.Utilities;

namespace KERRY.NMS.KEFIREBASEADMIN
{
    public class KEFirebaseAdmin
    {
        public String _AppName { get; set; }

        public KEFirebaseAdmin(String AppName)
        {
            try
            {
                if (FirebaseApp.GetInstance(name: AppName) == null) 
                {
                    string pathJSON = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"app_services.json", "");
                    string pathJSONFirebase = "";

                    using (StreamReader file = File.OpenText(pathJSON))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);
                        try
                        {
                            pathJSONFirebase = o2[AppName].ToString();
                        }
                        catch (Exception e)
                        {
                            throw new Exception(KEFirebaseErrorMessage.NO_APP);
                        }

                    }
                    Console.WriteLine("pathJSONFirebase: " + pathJSONFirebase);

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathJSONFirebase, "");
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(path),
                    }, AppName);
                }
                _AppName = AppName;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> sendNotificationByTopic(string NotificationTypeMode, String topic, Dictionary<String,String> data, Notification notification, string androidIcon = null, string androidColor = null, int apnsBadge = -1)
        {
            var returnStr = "";
            try
            {
                var message = new Message();

                //type of notification needed
                if (NotificationTypeMode == NotificationType.DATA)
                {
                    message.Data = data;
                }
                if (NotificationTypeMode == NotificationType.NOTIFICATION)
                {
                    message.Notification = notification;
                }
                if (NotificationTypeMode == NotificationType.BOTH)
                {
                    if (data != null)
                    {
                        message.Data = data;
                    }
                    message.Notification = notification;
                }
                else 
                {
                    throw new Exception(KEFirebaseErrorMessage.WRONG_NOTIFICATION_TYPE_MODE);
                }


                if (topic != null)
                {
                    message.Topic = topic;
                }
                else
                    throw new Exception(KEFirebaseErrorMessage.EMPTY_TOPIC);

                if (!string.IsNullOrEmpty(androidIcon) || !string.IsNullOrEmpty(androidColor))
                {
                    message.Android = new AndroidConfig()
                    {
                        Notification = new AndroidNotification()
                        {
                            Icon = androidIcon == null ? "" : androidIcon,
                            Color = androidColor == null ? "" : androidColor,
                        }
                    };
                }

                //APNs Config
                if (apnsBadge != -1)
                {
                    message.Apns = new ApnsConfig()
                    {
                        Aps = new Aps()
                        {
                            Badge = apnsBadge
                        },

                    };
                }

                string response = await FirebaseMessaging.GetMessaging(
                    FirebaseApp.GetInstance(name: _AppName)
                    ).SendAsync(message);
                returnStr = $"Successfully sent message to app: {_AppName} with topic: {topic}";
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                Console.WriteLine("sendNotificationByTopic Failed: " + errorMsg);

                string msg = ex.Message;
                if (ex.InnerException != null)
                {

                    msg += " <br>@ " + ex.InnerException.StackTrace;
                    msg += " <br>@ " + ex.InnerException.Message;

                    if (ex.InnerException.InnerException != null)
                    {

                        msg += " <br>@ " + ex.InnerException.InnerException.StackTrace;
                        msg += " <br>@ " + ex.InnerException.InnerException.Message;

                    }
                }
                Console.WriteLine(returnStr + msg);
                throw new Exception(ex.Message);
            }
            return returnStr;
        }
        public async Task<string> sendNotificationByTokens(string NotificationTypeMode, List<string> Tokens, Dictionary<String, String> data, Notification notification, string topic = null, string androidIcon = null, string androidColor = null, int apnsBadge = -1)
        {
            var returnStr = "";
            try
            {
                var message = new MulticastMessage();

                //type of notification needed
                if (NotificationTypeMode == NotificationType.DATA)
                {
                    message.Data = data;
                }
                if (NotificationTypeMode == NotificationType.NOTIFICATION)
                {
                    message.Notification = notification;
                }
                if (NotificationTypeMode == NotificationType.BOTH)
                {
                    if (data != null)
                    {
                        message.Data = data;
                    }
                    message.Notification = notification;
                }
                else
                {
                    throw (new Exception(KEFirebaseErrorMessage.WRONG_NOTIFICATION_TYPE_MODE));
                }

                if (!Tokens.Any()) 
                {
                    throw (new Exception(KEFirebaseErrorMessage.EMPTY_TOKENS));
                }
                message.Tokens = Tokens;

                if (!string.IsNullOrEmpty(androidIcon) || !string.IsNullOrEmpty(androidColor))
                {
                    message.Android = new AndroidConfig()
                    {
                        Notification = new AndroidNotification()
                        {
                            Icon = androidIcon == null ? "" : androidIcon,
                            Color = androidColor == null ? "" : androidColor,
                        }
                    };
                }

                //APNs Config
                if (apnsBadge != -1)
                {
                    message.Apns = new ApnsConfig()
                    {
                        Aps = new Aps()
                        {
                            Badge = apnsBadge
                        },

                    };
                }

                BatchResponse response = await FirebaseMessaging.GetMessaging(
                    FirebaseApp.GetInstance(name: _AppName)
                    ).SendMulticastAsync(message);

                if (response.FailureCount > 0)
                {
                    var failedTokens = new List<string>();
                    for (var i = 0; i < response.Responses.Count; i++)
                    {
                        if (!response.Responses[i].IsSuccess)
                        {
                            // The order of responses corresponds to the order of the registration tokens.
                            failedTokens.Add(Tokens[i]);
                        }
                    }
                    returnStr = string.Join(",\n  ", failedTokens.ToArray());
                    Console.WriteLine("sendToDevicesList Failure for: ");
                    Console.WriteLine($"List of tokens that caused failures: \n  {returnStr}");
                }

                returnStr = $"Sent batch message to app: {_AppName}, Success: {response.SuccessCount}, Failure: {response.FailureCount}";
            }
            catch (Exception e)
            {
                var errorMsg = e.Message;
                Console.WriteLine("sendToDevicesList Failure sent message: " + errorMsg);
                string msg = e.Message;
                if (e.InnerException != null)
                {

                    msg += " <br>@ " + e.InnerException.StackTrace;
                    msg += " <br>@ " + e.InnerException.Message;

                    if (e.InnerException.InnerException != null)
                    {

                        msg += " <br>@ " + e.InnerException.InnerException.StackTrace;
                        msg += " <br>@ " + e.InnerException.InnerException.Message;

                    }
                }
                returnStr = msg;
            }
            return returnStr;
        }
    }
}
