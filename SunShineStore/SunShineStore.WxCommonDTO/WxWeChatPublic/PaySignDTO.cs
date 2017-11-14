using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonDTO.WxWeChatPublic
{
    public class PaySignDTO
    {
        /// <summary>
        /// 公众号名称，由商户传入    
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 时间戳，自1970年以来的秒数       
        /// </summary>
        public string timeStamp { get; set; }

        /// <summary>
        /// 随机串    
        /// </summary>
        public string nonceStr { get; set; }

        /// <summary>
        /// 订单详情扩展字符串    
        /// </summary>
        public string package { get; set; }
        /// <summary>
        ///  微信签名方式   
        /// </summary>
        public string signType { get; set; }
        /// <summary>
        ///  微信签名   
        /// </summary>
        public string paySign { get; set; } 
    }
}
