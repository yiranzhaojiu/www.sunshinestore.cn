using Strore_Common.Component;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WxProject.Controllers
{
    public class WxBottomMenuController : Controller
    {
        //
        // GET: /WxBottomMenu/

        public ActionResult Index()
        {
            var services = LocalServiceLocator.GetService<IWxBottomMenuServices>();
            //services.UpdateBottomMen();
            return View();
        }

    }
}
