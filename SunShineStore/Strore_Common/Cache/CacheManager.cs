using Strore_Common.Caching.Memcached;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Strore_Common.Cache
{ 
 
    /// <summary>
    /// 缓存管理类
    /// </summary>
    public class CacheManager
    {
        private static bool IsOldMemcachedClient =true;//bool.Parse(System.Configuration.ConfigurationManager.AppSettings["IsOldMemcachedClient"]);

        private static bool EnableMemCache = true;// bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableMemCache"]);

        /// <summary>
        /// 缓存是否存在(默认MemCache缓存)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>是否存在</returns>
        public static bool KeyExists(string key)
        {
            CacheType cacheType = CacheType.MemCached;
            return KeyExists(key, cacheType);
        }

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="cacheType">缓存类型</param>
        /// <returns>是否存在</returns>
        public static bool KeyExists(string key, CacheType cacheType)
        {
            if (cacheType == CacheType.MemCached && EnableMemCache)
            {
                if (IsOldMemcachedClient)
                {
                    OwnMemcached memCached = MemObject.StaticMec;
                    return memCached.KeyExists(key);
                }
                else
                {
                    MemcachedClientCache memCacheNew = MemObjectNew.StaticMecNew;
                    return memCacheNew.KeyExists(key);
                }
            }
            else
            {
                return NetCache.KeyExists(key);
            }
        }

        /// <summary>
        /// 获取缓存值(默认MemCache缓存)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>缓存值</returns>
        public static object Get(string key)
        {
            CacheType cacheType = CacheType.MemCached;
            return Get(key, cacheType);
        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="cacheType">缓存类型</param>
        /// <returns>缓存值</returns>
        public static object Get(string key, CacheType cacheType)
        {
            if (cacheType == CacheType.MemCached && EnableMemCache)
            {
                if (IsOldMemcachedClient)
                {
                    OwnMemcached memCached = MemObject.StaticMec;
                    return memCached.Get(key);
                }
                else
                {
                    MemcachedClientCache memCacheNew = MemObjectNew.StaticMecNew;
                    return memCacheNew.Get(key);
                }
            }
            else
            {
                return NetCache.Get(key);
            }
        }

        /// <summary>
        /// 新客户端，直接返回，键值对，可直接使用Get<T>
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [Obsolete("使用 Get<T>(string[] keys)直接返回Dictionary<string,T>")]
        public static Hashtable Get(string[] keys)
        {
            if (IsOldMemcachedClient)
            {
                OwnMemcached memCached = MemObject.StaticMec;
                return memCached.GetMultiple(keys);
            }
            else
            {
                MemcachedClientCache memCacheNew = MemObjectNew.StaticMecNew;
                var result = memCacheNew.GetAll(keys);
                var returnValue = new Hashtable();
                foreach (var kv in result)
                {
                    returnValue.Add(kv.Key, kv.Value);
                }
                return returnValue;
            }
        }

        public static Dictionary<string, T> Get<T>(string[] keys)
        {
            Dictionary<string, T> values = new Dictionary<string, T>();
            if (IsOldMemcachedClient)
            {
                OwnMemcached memCached = MemObject.StaticMec;
                var hashtable = memCached.GetMultiple(keys);


                foreach (DictionaryEntry item in hashtable)
                {
                    try
                    {
                        values[item.Key.ToString()] = (T)item.Value;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                MemcachedClientCache memCacheNew = MemObjectNew.StaticMecNew;
                values = (Dictionary<string, T>)memCacheNew.GetAll<T>(keys);
            }

            return values;
        }

        /// <summary>
        /// 设置缓存(默认MemCache缓存,缓存1h)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存value</param>
        public static void Set(string key, object obj)
        {
            CacheType cacheType = CacheType.MemCached;
            Set(key, obj, DateTime.Now.AddHours(1), cacheType);
        }

        /// <summary>
        /// 设置缓存(默认MemCache缓存)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存value</param>
        /// <param name="validateTime">绝对过期时间</param>
        public static void Set(string key, object obj, DateTime validateTime)
        {
            CacheType cacheType = CacheType.MemCached;
            Set(key, obj, validateTime, cacheType);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存value</param>
        /// <param name="validateTime">绝对过期时间</param>
        /// <param name="cacheType">缓存类型</param>
        public static void Set(string key, object obj, DateTime validateTime, CacheType cacheType)
        {
            if (cacheType == CacheType.MemCached && EnableMemCache)
            {
                if (IsOldMemcachedClient)
                {
                    OwnMemcached memCached = MemObject.StaticMec;
                    memCached.Set(key, obj, validateTime);
                }
                else
                {
                    MemcachedClientCache memCacheNew = MemObjectNew.StaticMecNew;
                    memCacheNew.Set(key, obj, validateTime);
                }
            }
            else
            {
                NetCache.Set(key, obj, validateTime);
            }

        }

        /// <summary>
        /// 文件依赖缓存(只支持Net缓存)
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存值</param>
        /// <param name="validateTime">过期时间</param>
        /// <param name="cachedep">缓存以来对象</param>
        public static void Set(string key, object obj,DateTime validate, CacheDependency cachedep)
        {
            NetCache.Set(key, obj, cachedep,validate);
        }

        /// <summary>
        /// 删除缓存（默认MemCache缓存）
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>操作结果</returns>
        public static void Delete(string key)
        {
            CacheType cacheType = CacheType.MemCached;
            Delete(key, cacheType);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="cacheType">缓存类型</param>
        /// <returns>操作结果</returns>
        public static void Delete(string key, CacheType cacheType)
        {
            if (cacheType == CacheType.MemCached && EnableMemCache)
            {
                if (IsOldMemcachedClient)
                {
                    OwnMemcached memCached = MemObject.StaticMec;
                    memCached.Delete(key);
                }
                else
                {
                    MemcachedClientCache memCacheNew = MemObjectNew.StaticMecNew;
                    memCacheNew.Remove(key);
                }
            }
            else
            {
                NetCache.Delete(key);
            }
        }
         
    }
}
