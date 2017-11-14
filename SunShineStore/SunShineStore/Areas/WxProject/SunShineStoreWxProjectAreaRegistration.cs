using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WxProject
{
    public class SunShineStoreWxProjectAreaRegistration : System.Web.Mvc.AreaRegistration
    {
        public override string AreaName
        {
            get { return "WxProject"; }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            context.MapRoute("WxProject_Default", "WxProject/{controller}/{action}/{id}", new { controller = "WxProject", action = "index", id = System.Web.Mvc.UrlParameter.Optional });
        }
    }
}