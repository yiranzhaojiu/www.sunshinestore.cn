using Store.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace SunShineStoreApi.CommonFilter
{
    public class SimpleContentNegotiator : IContentNegotiator
    {
        private readonly XmlFormatter xmlFormatter = new XmlFormatter();

        public ContentNegotiationResult Negotiate(Type type, System.Net.Http.HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        { 
            //返回json字符串
            if (type.BaseType == typeof(StringDTO))
            {
                var formatter = formatters.FirstOrDefault(r => r is System.Net.Http.Formatting.JsonMediaTypeFormatter);

                if (formatter.CanWriteType(type))
                {
                    var mt = formatter.SupportedMediaTypes.FirstOrDefault(i => i.MediaType == "application/json");

                    if(mt!=null){
                        return new ContentNegotiationResult(formatter.GetPerRequestFormatterInstance(type, request, null), mt);
                    }
                }
            } 
            
            return new ContentNegotiationResult(xmlFormatter.GetPerRequestFormatterInstance(type, request, null), new MediaTypeHeaderValue("application/xml"));
        }
    }
}