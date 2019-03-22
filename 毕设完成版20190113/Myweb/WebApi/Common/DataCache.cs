using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Common
{
    /// <summary>
    /// 缓存相关的操作类
    /// Copyright (C) Maticsoft
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">Cache值名称</param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }
        /// <summary>
        /// 读取一下是否存在
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCacheTorF(string CacheKey)
        {
            var objCache = HttpRuntime.Cache.Get(CacheKey);
            return objCache;
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">Cache值名称</param>
        /// <param name="objObject">Cache值对象</param>
        public static void SetCache(string CacheKey, object objObject)//, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null);//, absoluteExpiration, slidingExpiration);

        }
        /// <summary>
        /// 弹性过期时间，当缓存没使用1小时就过期的Cache值
        /// </summary>
        /// <param name="CacheKey">Cache值名称</param>
        /// <param name="objObject">Cache值对象</param>
        public static void SetCacheOneHours(string CacheKey, object objObject)//, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null,System.Web.Caching.Cache.NoAbsoluteExpiration,TimeSpan.FromHours(1));//FromSeconds(10), absoluteExpiration, slidingExpiration);

        }
        /// <summary>
        /// 弹性过期时间，当缓存超過5分钟没使用就过期的Cache值
        /// </summary>
        /// <param name="CacheKey">Cache值名称</param>
        /// <param name="objObject">Cache值对象</param>
        public static void SetCacheMinutes(string CacheKey, object objObject)//, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));//.FromHours(1));//FromSeconds(10), absoluteExpiration, slidingExpiration);

        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveCacheByCacheKey(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}
