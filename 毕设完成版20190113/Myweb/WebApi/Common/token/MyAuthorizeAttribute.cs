using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Security;

namespace Common
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 废弃了作为原版保留一下
        /// </summary>
        private readonly string TimeStamp = ConfigurationManager.AppSettings["TimeStamp"];


        /// <summary>
        /// 验证入口
        /// </summary>
        /// <param name="filterContext"></param>override
        public  void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }


        /// <summary>
        /// 验证核心代码
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>override
        protected  object AuthorizeCore(LoginUserInfo model)//HttpContextBase httpContext,
        {
             TMessage<LoginUserInfo> mes = new TMessage<LoginUserInfo>();

            //前端请求api时会将token存放在名为"auth"的请求头中
            //沒有则错误
            var authHeader = model.Token; //httpContext.Request.Headers["auth"];
            if (authHeader == null) { 
               // httpContext.Response.StatusCode = 403;
               
                mes.suc = false;
                mes.mes = ConstHelper.TOKEN_ISNULL;
                return mes;
                //return false;
            }
            //请求参数
            string rtime=DESCryption.Encode(DateTime.Now.ToString());
            //要用户传时间的值进去
            string requestTime = model.ttime;//httpContext.Request["rtime"]; //请求时间经过DESC签名
            if (string.IsNullOrEmpty(requestTime))
                mes.suc = false;
                mes.mes = ConstHelper.TIME_ISNULL;
                return mes;


            //请求时间RSA解密后加上时间戳的时间即该请求的有效时间
            DateTime Requestdt = DateTime.Parse(DESCryption.Decode(requestTime)).AddMinutes(int.Parse(TimeStamp));
            DateTime Newdt = DateTime.Now; //服务器接收请求的当前时间
            if (Requestdt < Newdt)//超出時間过期(60分钟)安全退出
            {
                //跳转到登录页面
                mes.suc = false;
                mes.mes = ConstHelper.TOKEN_TIMEOUT;
                return mes;
            }
                //进行其他操作
                var userinfo = JwtHelp.GetJwtDecode(authHeader);
                //举个例子  生成jwtToken 存入redis中    
                //这个地方用jwtToken当作key 获取实体val   然后看看jwtToken根据redis是否一样
                //if (userinfo.UserName == "admin" && userinfo.PassWord == "123")
                //    return true;
            if(DataCache.GetCache(model.UserName).Equals(model.Token)){
                return true;
            }
            return false;
        }

        

        /// <summary>
        /// 验证失败处理
        /// </summary>
        /// <param name="filterContext"></param>override
        protected  void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("/Error");
            filterContext.HttpContext.Response.Redirect("/Home/Error");
        }
    }
}




