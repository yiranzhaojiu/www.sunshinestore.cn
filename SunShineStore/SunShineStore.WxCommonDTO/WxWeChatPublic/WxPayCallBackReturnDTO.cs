using SunShineStore.WxCommonEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonDTO.WxWeChatPublic
{
    public class WxPayCallBackReturnDTO
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public WxPayCallBackRentnCodeEnum return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }
    }
}
