using Strore_Common.Utility;
using Strore_Common.WxHelper;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Impl
{
    public class WxServerConfigServices : IWxServerConfigServices
    {
        public string WxAppid = CommonHelper.GetConfigData("WxAppID", "");
        public string WxAppSecret = CommonHelper.GetConfigData("WxAppSecret", "");
        public string WxToken = CommonHelper.GetConfigData("WxToken", "");

        /// <summary>
        /// 验证请求是否合法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool WxUrlValidate(WxServiceConfigDTO model)
        {
            //字典排序后的字符串
            string orderStr = WxCommonHelper.DictionaryOrder(model.timestamp, WxToken, model.nonce);
            //加密字符串
            string _signature = WxCommonHelper.EncryptShaOne(orderStr);

            if (string.IsNullOrEmpty(_signature))
                return false;

            if (string.Compare(_signature,model.signature,false)==1)
                return true;

            return false;
        }
    }
}
