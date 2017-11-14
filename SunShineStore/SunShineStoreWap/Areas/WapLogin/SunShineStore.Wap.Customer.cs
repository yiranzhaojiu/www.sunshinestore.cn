using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.Customer
{
    public class SunShineStoreWapCustomer : AreaRegistration
    {

        public override string AreaName
        {
            get { return "WapLogin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WapLogin_Default",
                "WapLogin/{controller}/{action}/{id}",
                new {  action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}