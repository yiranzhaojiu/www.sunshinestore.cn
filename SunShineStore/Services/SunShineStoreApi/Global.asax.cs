using Strore_Common;
using SunShineStoreApi.CommonFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SunShineStoreApi
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //ContentNegotiator， 简化选择流程。多匹配xml
            GlobalConfiguration.Configuration.Services.Replace(typeof(IContentNegotiator), new SimpleContentNegotiator());
            //启动IOC容器
            StoreFramework.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //清理IOC容器
            StoreFramework.End();
        }
    }
}