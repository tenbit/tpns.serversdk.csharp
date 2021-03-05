using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ThirdParty.Tpns
{
    public class TpnsApp
    {
        public static string RESTAPI_PUSHSINGLEDEVICE = "https://api.tpns.tencent.com/v3/push/app";
       
        private long m_accessId;
        private string m_secretKey;

        public TpnsApp()
        {
           
        }


        private string generateSign(PushData pushData)
        {
            string ret;

            string timeStampSt;
            string accessId;
            string requestBody;
            string keySecret;

            timeStampSt = pushData.timeStamp.ToString();
            accessId = this.m_accessId.ToString();
            keySecret = this.m_secretKey;
            requestBody = pushData.bodyJson;

            ret = GenSign.genSign(timeStampSt, accessId, requestBody, keySecret);

            return ret;
        }

        private string callRestful(string url, PushData pushData)
        {
            string ret;
            ret = "";

            string sign;
            sign = generateSign(pushData);
            if (sign.Length == 0)
                return "generate sign error";

            //url = "http://192.168.1.100/http/about?cityid=255";
            //url = "https://www.lovejiajiao.com/http/about";

            try
            {

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.ContentType = "application/json";
                req.Method = "POST";
                req.Timeout = 20000;

                req.Headers.Add("AccessId", this.m_accessId.ToString());
                req.Headers.Add("TimeStamp", pushData.timeStamp.ToString());
                req.Headers.Add("Sign", sign);

                string requestBody = pushData.bodyJson;

                //req.ContentType = "application/json; charset=UTF-8";
                //req.Headers.Add("Content-Type", "application/json");
                if (0 < requestBody.Length)
                {
                    byte[] data = Encoding.UTF8.GetBytes(requestBody);
                    using (Stream stream = req.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)req.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                string responseContent = streamReader.ReadToEnd();

                httpWebResponse.Close();
                streamReader.Close();

                ret = responseContent;
            }
            catch (Exception e)
            {
                ret = e.ToString();
            }

            return ret;
        }

        public string PushSingleDevice(PushData pushData)
        {
            string ret;
            this.m_accessId = pushData.accessID;
            this.m_secretKey = pushData.secretKey;

            Body body = new Body();
            body.setToken(pushData.token);
            body.setType(pushData.m_type);

            Message message = new Message();
            message.setType(pushData.m_type);
            body.message = message;

            //HttpUtility.UrlEncode
            message.setTitle(pushData.title);
            message.setContent((pushData.content));

            message.mAndroid.setAction(pushData.action);
            message.mAndroid.setCustom(pushData.custom);

            pushData.bodyJson = body.toJson();
            ret = callRestful(TpnsApp.RESTAPI_PUSHSINGLEDEVICE, pushData);

            return ret;

        }

    }
}
