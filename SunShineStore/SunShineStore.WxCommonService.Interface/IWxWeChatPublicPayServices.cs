using Store.BaseDTO;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonDTO.WxWeChatPublic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Interface
{
    /// <summary>
    /// 微信公众号支付
    /// </summary>
    public interface IWxWeChatPublicPayServices
    {
        /// <summary>
        /// 统一下单接口
        /// </summary>
        ExecuteResult<PaymentRequestDTO> UnifytheSingleInterface();

        /// <summary>
        /// 
        /// </summary>
        ExecuteResult<WxPayCallBackReturnDTO> CallBackSureOrder();
    }
}
