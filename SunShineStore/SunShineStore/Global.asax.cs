using Strore_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SunShineStore
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); 
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //启动IOC容器
            StoreFramework.Start();
        }

        protected void Application_End(Object sender, EventArgs e)
        {
            //清理IOC容器
            StoreFramework.End();
        }
    }
}