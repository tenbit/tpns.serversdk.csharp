using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThirdParty.Tpns
{
    public class Body
    {
        public static int TYPE_NOTIFICATION = 1;
        public static int TYPE_MESSAGE = 2;

        public string audience_type;

        private string[] token_list;

        public Message message;
        public string message_type;

        public string environment;

        public int upload_id;


        public Body()
        {
            this.audience_type = "token";
            
        }

         
        public void setType(int type)
        {
            this.message_type = TYPE_NOTIFICATION==type?"notify":"message";

        }

        public void setToken(string token)
        {
            this.token_list = new string[] { token };

        }

        public string toJson()
        {
            string ret;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            
            dict.Add("audience_type", audience_type);
            dict.Add("token_list", this.token_list);
            dict.Add("message_type", message_type);
            dict.Add("message", message.toJson());

            //dict.Add("environment", "product");
            //dict.Add("platform", "android");
            //dict.Add("upload_id", DateTime.Now.Ticks);
            dict.Add("upload_id", 105);

            ret = JsonConvert.SerializeObject(dict);
            return ret;
        }


    }
}
