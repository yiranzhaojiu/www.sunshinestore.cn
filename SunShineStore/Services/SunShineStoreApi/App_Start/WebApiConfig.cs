using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SunShineStoreApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultWxPrjApi",
                routeTemplate: "api/WxProject/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                namespaces: new string[] { "SunShineStoreApi.Controllers.WxProject" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultOtherPrjApi",
                routeTemplate: "api/OtherPrj/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                namespaces: new string[] { "SunShineStoreApi.Controllers.OtherPrj" }
            );

            config.Filters.Clear();
        }

        /// <summary>
        /// 对HttpRouteCollection.MapHttpRoute方法的扩展，增加对namespaces的支持
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="name"></param>
        /// <param name="routeTemplate"></param>
        /// <param name="defaults"></param>
        /// <param name="namespaces"></param>
        private static void MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            var defaultsDictionary = new HttpRouteValueDictionary(defaults);
            var dataTokens = new HttpRouteValueDictionary(new { Namespace = namespaces });
            IHttpRoute route = routes.CreateRoute(routeTemplate, defaultsDictionary, null, dataTokens, null);
            routes.Add(name, route);
        }
    }
}
