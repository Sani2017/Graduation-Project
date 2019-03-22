using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;

namespace Common
{
    public class SmsHelper
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phoneNumbers">手机号码（可群发，最多200个。不过发送内容相同不适用于验证码发送，需要另写方法）</param>
        /// <param name="random">生成验证码（建议6位）</param>
        public void PutSms(string[] phoneNumbers)
        {
            string random = "";
            for (int i = 0; i < phoneNumbers.LongLength; i++)
            {
                Random rd = new Random();　　//无参即为使用系统时钟为种子
                random=rd.Next(100000, 1000000).ToString();
                // 短信应用SDK AppID
                int appid = 1400149678;
                // 短信应用SDK AppKey
                string appkey = "9ad6f7a8193d61cbca39e8f0ff79d57a";//"9ff91d87c2cd7cd0ea762f141975d1df37481d48700d70ac37470aefc60f9bad";	
                // 需要发送短信的手机号码
                //string[] phoneNumbers = { "15397097324", "18368718477" };

                // 短信模板ID，需要在短信应用中申请
                int templateId = 241444; // NOTE: 这里的模板ID`7839`只是一个示例，真实的模板ID需要在短信控制台中申请
                //templateId 7839 对应的内容是"您的验证码是: {1}"
                // 签名
                string smsSign = "蒲公英分享"; // NOTE: 这里的签名只是示例，请使用真实的已申请的签名, 签名参数使用的是`签名内容`，而不是`签名ID`
                try
                {
                    SmsMultiSender msender = new SmsMultiSender(appid, appkey);//现在可支持群发。SmsSingleSender为单发短信phoneNumbers[0]
                    var sresult = msender.sendWithParam("86", phoneNumbers, templateId,
                        new[] { random }, smsSign, "", "");  //"5678" 签名参数未提供或者为空时，会使用默认签名发送短信
                    Console.WriteLine(sresult);
                    string cachePhoneNumber = phoneNumbers[0];//只有一個手機號
                    DataCache.SetCacheMinutes(cachePhoneNumber, random);//手機號作爲該緩存的名字，驗證成功刪除緩存。
                }
                catch (JSONException e)
                {
                    Console.WriteLine(e);
                }
                catch (HTTPException e)
                {
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

    }
}
