using Strore_Common.Component;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStoreWap.Controllers
{
    public class WxServerConfigController : Controller
    {
        //
        // GET: /WxServerConfig/

        /// <summary>
        /// 微信服务器配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ServiceConfig(WxServiceConfigDTO model)
        {
              
            if (string.IsNullOrEmpty(model.echostr) || string.IsNullOrEmpty(model.nonce) || string.IsNullOrEmpty(model.signature) || string.IsNullOrEmpty(model.timestamp))
            {
                ViewBag.status = false;
            }
            else
            {
                var services = LocalServiceLocator.GetService<IWxServerConfigServices>();
                if (services.WxUrlValidate(model))
                    return Content(model.echostr);
                return View(model);
            }

            return View(model);
        }

    }
}
