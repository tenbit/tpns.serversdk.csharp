using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThirdParty.Tpns
{
    public class Android
    {

        private ClickAction m_action;

        private Dictionary<string, object> m_custom;


        //private int m_builderId;
        //private int m_ring;
        //private int m_vibrate;
        //private int m_clearable;
        //private int m_nId;
        //private int m_lights;
        //private int m_iconType;
        //private int m_styleId;
        //private string m_ringRaw;
        //private string m_iconRes;
        //private string m_smallIcon;

        public void setAction(ClickAction action)
        {
            this.m_action = action;
        }

        public void setCustom(Dictionary<String, Object> custom)
        {
            this.m_custom = custom;
        }

        public Dictionary<string, object> toJson()
        {
         
            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (null != m_action)
            {
                dict.Add("action", m_action.toJson());
            }

            if (null != m_custom)
            {
                dict.Add("custom_content", JsonConvert.SerializeObject(m_custom));
            }

            return dict;
        }

    }
}
