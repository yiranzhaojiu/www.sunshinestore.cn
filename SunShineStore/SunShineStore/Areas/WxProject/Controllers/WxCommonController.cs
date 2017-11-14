using Strore_Common.Component;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WxProject.Controllers
{
    public class WxCommonController : Controller
    {
        //
        // GET: /WxCommon/

        public ActionResult Index(WxServiceConfigDTO model)
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
