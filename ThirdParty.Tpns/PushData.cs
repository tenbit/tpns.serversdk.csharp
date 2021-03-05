using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty.Tpns
{
    public class PushData
    {
        public int m_type;

        public string title;
        public string content;

        public string token;

        public ClickAction action;
        public Dictionary<string, object> custom;

        public long accessID;

        public string secretKey;

        public long timeStamp;

        public string bodyJson;
    }
}
