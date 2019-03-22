using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class JwtController : Controller//ApiController//
    {
        /// <summary>
        /// 生成jwt Token
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public ActionResult CreateToken(string username, string pwd)
        {

            DataResult result = new DataResult();

            //假设用户名为"admin"，密码为"123"  
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(pwd))//(username == "admin123" && pwd == "123")
            {

                var payload = new Dictionary<string, object>
                {
                    { "username",username },
                    { "pwd", pwd }
                };

                result.Token = JwtHelp.SetJwtEncode(payload);
                result.Success = true;
                result.Message = "成功";
            }
            else
            {
                result.Token = "";
                result.Success = false;
                result.Message = "生成token失败";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
