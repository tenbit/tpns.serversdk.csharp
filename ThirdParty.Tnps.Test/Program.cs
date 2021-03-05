using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ThirdParty.Tpns;

namespace ThirdParty.Tnps.Test
{
    class Program
    {

        //only for android device
        static void Main(string[] args)
        {
            TpnsApp pushApp = new TpnsApp();

            PushData pushData = new PushData();
            pushData.m_type = Body.TYPE_NOTIFICATION;

            pushData.accessID = 1500014317;//replace it by your accessID
            pushData.secretKey = "xxx";//replace it by your secret key
            pushData.token = "00ce7e5bb088f6f9065b5ba9a7b191fda19a";//replace it by your token

            //HttpUtility.UrlEncode
            pushData.title = HttpUtility.UrlEncode("mytitle中");
            pushData.content = "my body";

            //pushData.custom = custom;
            //pushData.action = action;

            pushData.timeStamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;

            string ret = pushApp.PushSingleDevice(pushData);

            Console.WriteLine("");
            Console.WriteLine(ret);
            
            Console.ReadLine();
        }
    }
}
