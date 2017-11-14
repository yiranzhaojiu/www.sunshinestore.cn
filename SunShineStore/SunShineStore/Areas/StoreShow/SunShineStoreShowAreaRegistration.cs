using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Show
{
    public class SunShineStoreShowAreaRegistration:AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "StoreShow";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "StoreShow_Default",
               "StoreShow/{controller}/{action}/{id}",
               new { controller = "StoreShow", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}