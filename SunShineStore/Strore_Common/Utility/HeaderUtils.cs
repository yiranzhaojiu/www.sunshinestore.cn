using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Strore_Common.Utility
{
    public class HeaderUtils
    {
        #region GetHeaderStringValue 获取Header的字符串值
        /// <summary>
        /// 获取Header的字符串值
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="isUrlDecode"></param>
        /// <returns></returns>
        public static string GetHeaderStringValue(HttpRequestHeaders headers, string keyName, string defaultValue, bool isUrlDecode)
        {
            IEnumerable<string> values;
            if (headers == null)
            {
                return defaultValue;
            }
            if (headers.TryGetValues(keyName, out values))
            {
                if (isUrlDecode)
                {
                    return HttpUtility.UrlDecode(values.First());
                }
                return values.First();
            }
            return defaultValue;
        }
        /// <summary>
        /// 获取Header的字符串值
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="keyName"></param>
        /// <param name="isUrlDecode"></param>
        /// <returns></returns>
        public static string GetHeaderStringValue(HttpRequestHeaders headers, string keyName, bool isUrlDecode)
        {
            return GetHeaderStringValue(headers, keyName, string.Empty, isUrlDecode);
        }

        #endregion

        #region GetHeaderIntValue 获取Header的整型值
        /// <summary>
        /// 获取Header的整型值
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="isUrlDecode"></param>
        /// <returns></returns>
        public static int GetHeaderIntValue(HttpRequestHeaders headers, string keyName, int defaultValue, bool isUrlDecode)
        {
            IEnumerable<string> values;
            if (headers == null)
            {
                return defaultValue;
            }
            if (headers.TryGetValues(keyName, out values))
            {
                if (isUrlDecode)
                {
                    return CommonHelper.ObjectToInt(HttpUtility.UrlDecode(values.First()));
                }
                return CommonHelper.ObjectToInt(values.First());
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Header的整型值
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="keyName"></param>
        /// <param name="isUrlDecode"></param>
        /// <returns></returns>
        public static int GetHeaderIntValue(HttpRequestHeaders headers, string keyName, bool isUrlDecode)
        {
            return GetHeaderIntValue(headers, keyName, 0, isUrlDecode);
        }

        #endregion
    }
}
