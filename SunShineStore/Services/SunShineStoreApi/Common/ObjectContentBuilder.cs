using Fasterflect;
using SunShineStoreApi.CommonFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace SunShineStoreApi.Common
{
    public class ObjectContentBuilder
    {
        private static XmlFormatter formatter = new XmlFormatter();

        public static HttpResponseMessage Create(Type type, int code, string message, HttpRequestMessage request)
        {
            var responseMessage = new HttpResponseMessage();
            responseMessage.StatusCode = HttpStatusCode.OK;
            var obj = type.CreateInstance();
            obj.TrySetValue("IsSuccess", false);
            obj.TrySetValue("ErrorMsg", message);
            obj.TrySetValue("FailType", 1); 
            var cn = GlobalConfiguration.Configuration.Services.GetService(typeof(IContentNegotiator)) as IContentNegotiator;
            var cr = cn.Negotiate(type, request, GlobalConfiguration.Configuration.Formatters);
            responseMessage.Content = new ObjectContent(type, obj, cr.Formatter);
            return responseMessage;
        }
    }
}