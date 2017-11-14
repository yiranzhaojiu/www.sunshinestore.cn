using SunShineStore.WxCommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Interface
{
    public interface IWxLoginServices
    {
        /// <summary>
        /// 授权登陆链接
        /// </summary>
        /// <param name="redirect_uri"></param>
        /// <returns></returns>
        string GetLoginByWxUrl(string redirect_uri);

        /// <summary>
        /// 验证授权登陆的Code，并获取用户的信息
        /// </summary>
        /// <param name="ValideCode"></param>
        WxUserEntity ValideWxLoginCode(string ValideCode);
    }
}
