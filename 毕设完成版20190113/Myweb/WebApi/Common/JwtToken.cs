using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Common
{
    public  class JwtToken// : Controller//ApiController//
    {
        /// <summary>
        /// 生成jwt Token
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>ActionResult
        public static object CreateToken(string username, string password)
        {
            //DataResult result = new DataResult();
            string token = "";
            //假设用户名为"admin"，密码为"123"  
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))//(username == "admin123" && pwd == "123")
            {

                var payload = new Dictionary<string, object>
                {
                    { "username",username },
                    { "password", password },
                    {"date", DateTime.Now.Minute}
                };

               // result.
                    token = JwtHelp.SetJwtEncode(payload);
                //result.
                   // Success = true;
                //result.
                    //Message = "成功";

            //将token插入缓存中，60分钟弹性过期时间SetCacheOneHours
                //不退出登录一直存在
                DataCache.SetCache(username, token);
                    //https://zhidao.baidu.com/question/204760261.html
                    //https://www.cnblogs.com/lvjy-net/p/8297679.html#top
                    //https://blog.csdn.net/flyingdream123/article/details/80301496
                    //public static void SetCache(string CacheKey, object objObject)
                    //{
                    //System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                    //objCache.Insert(CacheKey, objObject);
                    //}
            //    ProductClass proClass = New ProductClass();
            //IList<ProductClassInfo> pci = proClass.GetProductClasses();

            //ProductClassCacheDependency pccd = new ProductClassCacheDependency();
            //CacheDependency cacheDepend = pccd.GetProductClassDependency();

            //Cache.Insert("ProductClass", pci, cacheDepend, Cache.NoAbsoluteExpiration, TimeSpan.FromMinut(30), CacheItemPriority.Normal, null); 
               
                
                
                //HttpRuntime.Cache.Insert(token.StaffId.ToString(),
                    //    token, null, token.ExpireTime,
                    //    TimeSpan.Zero);
            }
            else
            {
                //result.
                    token = "";
                //result.Success = false;
                //result.Message = "生成token失败";
            }

            return token; //Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
