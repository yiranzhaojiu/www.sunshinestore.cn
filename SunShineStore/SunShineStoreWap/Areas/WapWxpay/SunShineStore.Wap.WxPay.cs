using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.WxPay
{
	public class SunShineStoreWapWxPay:AreaRegistration
	{
        public override string AreaName
        {
            get { return "WapWxPay"; }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            context.MapRoute(
                "WapWxPay_Default",
                "WapWxPay/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}