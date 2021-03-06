using System;
using System.Security.Cryptography;
using System.Text;

namespace ThirdParty.Tpns
{
    public class GenSign
    {


        public static string HmacSHA256(string key, string data)
        {
            string hash;
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] code = encoder.GetBytes(key);
            using (HMACSHA256 hmac = new HMACSHA256(code))
            {
                Byte[] hmBytes = hmac.ComputeHash(encoder.GetBytes(data));
                hash = ToHexString(hmBytes);
            }
            return hash;
        }



        public static string ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static string genSign(string timeStampStr, string accessId, string requestBody, string keySecret)
        {
            string data = timeStampStr + accessId + requestBody;
            string hash = HmacSHA256(keySecret, data);
            string sign = Base64Encode(hash);
            Console.WriteLine("timeStampStr: " + timeStampStr + " accessId:" + accessId + " requestBody" + requestBody + " keySecret:" + keySecret);
            return sign;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
