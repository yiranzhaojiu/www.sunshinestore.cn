using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Strore_Common.Cache
{
    /// <summary>
    /// .NET缓存类
    /// </summary>
    internal class NetCache
    {
        /// <summary>
        /// 获取缓存值(默认MemCache缓存)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>缓存值</returns>
        public static object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="validateTime"></param>
        public static void Set(string key, object obj, DateTime validateTime)
        {
            HttpRuntime.Cache.Insert(key, obj, null, validateTime, TimeSpan.Zero);
        }

        /// <summary>
        /// 设置缓存依赖
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="CacheDepdency">缓存依赖对象</param>
        /// <param name="validateTime"></param>
        public static void Set(string key, object obj, CacheDependency CacheDepdency, DateTime validateTime)
        {
            HttpRuntime.Cache.Insert(key, obj, CacheDepdency, validateTime, TimeSpan.Zero);
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyExists(string key)
        {
            return HttpRuntime.Cache.Get(key) != null;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Delete(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

    }
}
