using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.CustomerDTO
{
    public class CusetomerUserInfoDTO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
         
        /// <summary>
        /// 验证码缓存值
        /// </summary>
        public string codeCacheKey { get; set; }

        /*
        /// <summary>
        /// 是否有可以操作后台的权限
        /// </summary>
        private int _BackGroundIsPreview = 0; 
        public int BackGroundIsPreview
        {
            get { return _BackGroundIsPreview; }
            set { value = _BackGroundIsPreview; }
        }
        */
    }
}
