using Memcached.ClientLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Cache
{
    /// <summary>
    /// MemCache缓存类
    /// </summary>
    internal class OwnMemcached
    {
        SockIOPool Pool;
        string[] serverlist;
        //memcached客户端对象
        MemcachedClient mc = new MemcachedClient();
        private string poolName;
        private string memServer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="poolName">缓存池名称</param>
        /// <param name="memServer">服务器列表</param>
        public OwnMemcached(string poolName,string memServer)
        {
            if (string.IsNullOrEmpty(poolName) || string.IsNullOrEmpty(memServer))
                return;

            var maxConnections = 40;
            if (!string.IsNullOrEmpty(Utility.CommonHelper.GetConfigData("MemMaxConnections")))
            {
                if (int.TryParse(Utility.CommonHelper.GetConfigData("MemMaxConnections"),
                    out maxConnections) == false)
                {
                    maxConnections = 40;
                }

                if (maxConnections < 40)
                    maxConnections = 40;
            }

            this.poolName = poolName;
            this.memServer = memServer;
           
            //初始化池
            Pool = SockIOPool.GetInstance(poolName);
            serverlist = memServer.Split(',');
            Pool.SetServers(serverlist);

            //初始化时创建连接数
            Pool.InitConnections = 5;
            //最小连接数
            Pool.MinConnections = 5;
            //最大连接数
            Pool.MaxConnections = maxConnections;

            //socket连接的超时时间，设置 0 表示不超时（单位ms），即一直保持链接状态
            Pool.SocketConnectTimeout = 1000;
            //通讯的超市时间，下面设置为3秒（单位ms），.Net版本没有实现
            Pool.SocketTimeout = 3000;
            //socket单次任务的最大时间（单位ms），超过这个时间socket会被强行中端掉，当前任务失败。
            Pool.MaxBusy = 1500;

            //维护线程的间隔激活时间，下面设置为30秒（单位s），设置为0时表示不启用维护线程
            Pool.MaintenanceSleep = 30;
            //设置SocktIO池的故障标志
            Pool.Failover = true;

            Pool.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;
            
            //是否对TCP/IP通讯使用nalgle算法，.net版本没有实现
            Pool.Nagle = false;
            Pool.Initialize();

            // 获得客户端实例
            mc.PoolName = poolName;
            //是否启用压缩数据：如果启用了压缩，数据压缩长于门槛的数据将被储存在压缩的形式
            mc.EnableCompression = false;
            //压缩设置，超过指定大小的都压缩 
            //cache.CompressionThreshold = 1024 * 1024;   
        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>缓存值</returns>
        public object Get(string key)
        {
            return mc.Get(key);
        }

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>是否存在</returns>
        public bool KeyExists(string key)
        {
            return mc.KeyExists(key);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="validateTime"></param>
        /// <returns></returns>
        public bool Set(string key, object obj, DateTime validateTime)
        {
           return mc.Set(key,obj,validateTime);
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="obj">缓存value</param>
        /// <returns>操作结果</returns>
        public bool Replace(string key, object obj)
        {
            return mc.Replace(key, obj);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>操作结果</returns>
        public bool Delete(string key)
        {
            return mc.Delete(key);
        }

        /// <summary>
        /// 获取当前Memcache服务器运行的状态
        /// </summary>
        /// <returns>MemCache状态信息</returns>
        public Hashtable Stats()
        {
            return mc.Stats();
        }

        /// <summary>
        /// 获取多个缓存key对应的值集合
        /// </summary>
        /// <param name="keys">缓存key集合</param>
        /// <returns>缓存value集合</returns>
        public Hashtable GetMultiple(string[] keys)
        {
            return mc.GetMultiple(keys);
        }
        /// <summary>
        /// 刷新所有Memcache服务器上保存的项目
        /// </summary>
        /// <returns>操作结果</returns>
        public bool FlushMemCache()
        {
            ArrayList alist = new ArrayList();
            for (int i = 0; i < serverlist.Length; i++)
            {
                alist.Add(serverlist[i]);
            }
            return mc.FlushAll(alist);
        }

        public void ShutDown()
        {
            Memcached.ClientLibrary.SockIOPool.GetInstance(mc.PoolName).Shutdown();
        }
    }

    internal class MemObject
    {
        private static OwnMemcached _staticMem;
        private static readonly object lockObj = new object();

        public static OwnMemcached StaticMec
        {
            get
            {
                if (_staticMem == null)
                {
                    lock (lockObj)
                    {
                        _staticMem = new OwnMemcached(WebHelper.WebHelper.MemcachedPoolName, WebHelper.WebHelper.MemcachedIPServices);
                    }
                }

                return _staticMem;
            }
        }
    }
}
