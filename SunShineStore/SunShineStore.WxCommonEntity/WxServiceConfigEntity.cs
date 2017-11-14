﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonEntity
{
    public class WxServiceConfigEntity
    {
        /// <summary>
        /// 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string nonce { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string echostr { get; set; } 
    }
}
