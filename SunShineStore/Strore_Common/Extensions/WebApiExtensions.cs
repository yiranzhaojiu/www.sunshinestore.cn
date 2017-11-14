using Strore_Common.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Strore_Common.Extensions
{
    public static class WebApiExtensions
    {
        [Flags]
        public enum LookScope
        {
            Headers = 2,
            Query = 4,
            Body = 8,
        }

        public static string GetValue(this HttpRequestMessage request, string key, LookScope lookScope = LookScope.Headers|LookScope.Query|LookScope.Body)
        {

            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            string value = null;

            // 1 headers

            if (lookScope.HasFlag(LookScope.Headers))
            {
                HttpRequestHeaders headers = request.Headers;
                value = HeaderUtils.GetHeaderStringValue(headers, key, true);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            // 2 query
            if (lookScope.HasFlag(LookScope.Query))
            {
                var queryString = request.RequestUri.Query;
                value = SplitValue(queryString, key);

                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
            // //3 body
            if (lookScope.HasFlag(LookScope.Body))
            {

                var bodyCacheKey = "PostBodysdljfsj123;";
                string bodyStr = null;
                var cacheBodyStr = HttpContext.Current.Items[bodyCacheKey];
                if (cacheBodyStr != null)
                {
                    bodyStr = cacheBodyStr.ToString();
                }
                else
                {
                    // modified: 异步环境下HttpContext.Current为null
                    var stream = request.Content.ReadAsStreamAsync().Result;
                    try
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                        bodyStr = sr.ReadToEnd();
                        HttpContext.Current.Items[bodyCacheKey] = bodyStr;

                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                    }
                     
                }
                value = SplitValue(bodyStr, key);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
             
            return null;  
        }

        private static string SplitValue(string queryString, string key)
        {

            if (string.IsNullOrEmpty(queryString))
            {
                return null;
            }
            queryString = queryString.TrimStart('?');
            key = key.ToLower();

            foreach (var part in queryString.Split('&'))
            {
                var kv = part.Split('=');
                if (kv.Length == 2 && kv[0].ToLower() == key)
                {
                    return HttpUtility.UrlDecode(kv[1], Encoding.UTF8);
                }
            }

            return null;

        }
    }
}
