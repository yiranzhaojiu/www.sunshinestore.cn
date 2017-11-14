using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.WxProject
{
    public class SunShineStoreWapWxProjectArea : AreaRegistration
    {
        public override string AreaName
        {
            get { return "WapWxProject"; }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            context.MapRoute(
                "WapWxProject_Default",
                "WapWxProject/{controller}/{action}/{id}",
                new { controller = "WxHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}