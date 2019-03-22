using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LoginTokenController : ApiController
    {
        private readonly string TimeStamp = ConfigurationManager.AppSettings["TimeStamp"];
        /// <summary>
        /// 用于用户浏览页面时的token验证
        /// </summary>
        /// <param name="model">登录用户所用的相关token值</param>
        /// <returns></returns>
        [Route("api/LoginToken/token")]
        public object token(LoginUserInfo model)//List<post>JsonResult
        {
            TMessage<LoginUserInfo> mes = new TMessage<LoginUserInfo>();

            //前端请求api时会将token存放在名为"auth"的请求头中
            //沒有则错误
            var authHeader = model.Token; //httpContext.Request.Headers["auth"];
            if (authHeader == null)
            {
                // httpContext.Response.StatusCode = 403;

                mes.suc = false;
                mes.mes = ConstHelper.TOKEN_ISNULL;
                return mes;
                //return false;
            }
            var tokenTorF = DataCache.GetCacheTorF(model.UserName);
            if (tokenTorF!=null)
            {
                if (DataCache.GetCache(model.UserName).Equals(model.Token))
                {
                    mes.suc = true;
                }
                else
                {
                    mes.suc = false;
                    mes.mes = ConstHelper.TOKEN_ISNULL;
                }
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.TOKEN_ISNULL +ConstHelper.USER_NOT_LOGIN;
                 
            }
            return mes;
            //请求参数
            // string rtime = DESCryption.Encode(DateTime.Now.ToString());20181207
            //要用户传时间的值进去
            //---------------------------------------------------------------------20181207
            //string requestTime = model.ttime;//httpContext.Request["rtime"]; //请求时间经过DESC签名
            //if (string.IsNullOrEmpty(requestTime)) {
            //    mes.suc = false;
            //    mes.mes = ConstHelper.TIME_ISNULL;
            //    return mes;
            // }

            //请求时间RSA解密后加上时间戳的时间即该请求的有效时间

            //------------------------------------------------------------------20181207
            //DateTime Requestdt = DateTime.Parse(DESCryption.Decode(requestTime)).AddMinutes(int.Parse(TimeStamp));
            //DateTime Newdt = DateTime.Now; //服务器接收请求的当前时间
            //if (Requestdt < Newdt)//超出時間过期(60分钟)安全退出
            //{
            //    //跳转到登录页面
            //    mes.suc = false;
            //    mes.mes = ConstHelper.TOKEN_TIMEOUT;
            //    return mes;
            //}


            //进行其他操作

            //var userinfo = JwtHelp.GetJwtDecode(authHeader);//这里是解析了token分析出用户名密码20181207
            
            //举个例子  生成jwtToken 存入redis中    
            //这个地方用jwtToken当作key 获取实体val   然后看看jwtToken根据redis是否一样
            //if (userinfo.UserName == "admin" && userinfo.PassWord == "123")
            //    return true;
        }
    }
}
