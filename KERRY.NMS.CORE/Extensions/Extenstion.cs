using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KERRY.NMS.CORE
{
    public static class Extenstion
    {
        private readonly static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string ToJsonCamel(this object objectConvert)
        {
            return JsonConvert.SerializeObject(objectConvert, serializerSettings);
        }

        public static T ToModelFromJsonCamel<T>(this string jsonCamel)
        {
            return JsonConvert.DeserializeObject<T>(jsonCamel, serializerSettings);
        }

        public static string MD5Hash(this string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static string FormatPhoneNumber_vi(this string mobileNumber)
        {
            if (mobileNumber != null && mobileNumber.Substring(0, 1) == "0")
            {
                mobileNumber = "84" + mobileNumber.Substring(1);
            }
            return mobileNumber;
        }
    }
}
