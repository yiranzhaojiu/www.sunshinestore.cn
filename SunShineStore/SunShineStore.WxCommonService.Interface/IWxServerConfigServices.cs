using SunShineStore.WxCommonDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Interface
{
    public interface IWxServerConfigServices
    {
        /// <summary>
        /// 微信公众平台服务器配置验证
        /// </summary>
        /// <returns></returns>
        bool WxUrlValidate(WxServiceConfigDTO model);
    }
}
