using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Cache
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// MemCache缓存（未开启用.NET缓存）
        /// </summary>
        MemCached = 0,
        /// <summary>
        /// NET缓存
        /// </summary>
        NetCache = 1
    }
}
