using Strore_Common.Component;
using Strore_Common.Log.Interface;
using Strore_Common.WxHelper;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonDTO.WxWeChatPublic;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.WxPay.Controllers
{
    public class WxPayController : Controller
    {
        //
        // GET: /WxPay/

        public ActionResult Index()
        {
            var services = LocalServiceLocator.GetService<IWxWeChatPublicPayServices>();
            var result = services.UnifytheSingleInterface();

            if (!result.IsSuccess)
            {
                return Content(result.ErrorMsg);
            }
            PaymentRequestDTO dto = result.ReturnValue;
            dto.appId = WxHelper.WxAppid; 
            dto.timeStamp = WxCommonHelper.GetUnixTime();
            dto.nonceStr = WxCommonHelper.GetNewGuidStr();
            dto.package = string.Concat("prepay_id=", dto.prepay_id);
            dto.signType = "MD5";  
            var dyobj = new PaySignDTO(); 
            dyobj.appId = dto.appId;
            dyobj.timeStamp = dto.timeStamp;
            dyobj.nonceStr = dto.nonceStr;
            dyobj.package = dto.package;
            dyobj.signType = dto.signType; 
            string signStr = WxCommonHelper.GetPaySignStr(dyobj) + "key=" + WxHelper.WxAPIPaySecret; 
            dto.paySign = MD5Util.MD5(signStr).ToUpper(); 
            return View(dto);
        }

        /// <summary>
        /// 微信回调处理订单
        /// </summary>
        /// <returns></returns> 
        [HttpPost]
        public ActionResult CallBackDealOrderInfo()
        {
            var services = LocalServiceLocator.GetService<IWxWeChatPublicPayServices>();
            var result=services.CallBackSureOrder(); 
            return Json(result.ReturnValue);
        }

        public void TestLog()
        {
            //LoggerManager.Error("客源预备库审核FangpPurpose异常", ex);

            ILogger Log = LocalServiceLocator.GetService<ILoggerFactory>().Create("memcache");
            Log.Error("测试LOG4gg");
        }
    }
}
