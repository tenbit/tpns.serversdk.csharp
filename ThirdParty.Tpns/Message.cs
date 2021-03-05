using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ThirdParty.Tpns
{
    public class Message
    {
        
        private string m_title;
        private string m_content;

        private List<TimeInterval> m_acceptTimes;


        private int m_expireTime;
        private string m_sendTime;
        
        private int m_type;
        private int m_multiPkg;
        
        

        private string m_raw;
        private int m_loopInterval;
        private int m_loopTimes;

        public Android mAndroid;

        public Message()
        {
            this.m_title = "";
            this.m_content = "";
            this.m_sendTime = "2013-12-20 18:31:00";
            this.m_acceptTimes = new List<TimeInterval>();
            this.m_multiPkg = 0;
            this.m_raw = "";
            this.m_loopInterval = -1;
            this.m_loopTimes = -1;

            mAndroid = new Android();
        }

        public void setTitle(String title)
        {
            this.m_title = title;
        }
        public void setContent(String content)
        {
            this.m_content = content;
        }
        public void setExpireTime(int expireTime)
        {
            this.m_expireTime = expireTime;
        }
        public int getExpireTime()
        {
            return this.m_expireTime;
        }
        public void setSendTime(String sendTime)
        {
            this.m_sendTime = sendTime;
        }
        public String getSendTime()
        {
            return this.m_sendTime;
        }
        public void addAcceptTime(TimeInterval acceptTime)
        {
            this.m_acceptTimes.Add(acceptTime);
        }
        public JArray acceptTimeToJsonArray()
        {
            JArray json = new JArray();
            foreach (TimeInterval ti in m_acceptTimes)
            {
                JObject jtemp = JObject.FromObject(ti.toJson());
                json.Add(jtemp);
            }
            return json;
        }
        public void setType(int type)
        {
            this.m_type = type;
        }
        public int getType()
        {
            return m_type;
        }
        public void setMultiPkg(int multiPkg)
        {
            this.m_multiPkg = multiPkg;
        }
        public int getMultiPkg()
        {
            return m_multiPkg;
        }
        
      
        public void setRaw(String raw)
        {
            this.m_raw = raw;
        }
        public int getLoopInterval()
        {
            return m_loopInterval;
        }
        public void setLoopInterval(int loopInterval)
        {
            m_loopInterval = loopInterval;
        }
        public int getLoopTimes()
        {
            return m_loopTimes;
        }
        public void setLoopTimes(int loopTimes)
        {
            m_loopTimes = loopTimes;
        }

        public Boolean isValid()
        {
            if (m_raw.Length != 0)
                return true;
            if (m_type < Body.TYPE_NOTIFICATION || m_type > Body.TYPE_MESSAGE)
                return false;
            if (m_multiPkg < 0 || m_multiPkg > 1)
                return false;
            
            if (m_expireTime < 0 || m_expireTime > 3 * 24 * 60 * 60)
                return false;
            foreach (TimeInterval ti in m_acceptTimes)
            {
                if (!ti.isValid()) return false;
            }
            if (m_loopInterval > 0 && m_loopTimes > 0 && ((m_loopTimes - 1) * m_loopInterval + 1) > 15)
            {
                return false;
            }
            return true;
        }

        public Dictionary<string, object> toJson()
        {
            
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Dictionary<string, object> message = new Dictionary<string, object>();
            if (m_type == Body.TYPE_NOTIFICATION)
            {
                dict.Add("title", m_title);
                dict.Add("content", m_content);
                //dict.Add("accept_time", acceptTimeToJsonArray());

                dict.Add("android", mAndroid.toJson());

            }
            else if (m_type == Body.TYPE_MESSAGE)
            {
                dict.Add("title", m_title);
                dict.Add("content", m_content);
                dict.Add("accept_time", acceptTimeToJsonArray());
            }
            return dict;
        }
    }


}
