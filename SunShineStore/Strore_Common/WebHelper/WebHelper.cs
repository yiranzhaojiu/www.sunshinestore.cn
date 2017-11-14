using Strore_Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Strore_Common.WebHelper
{
    /// <summary>
    /// Web通用类
    /// </summary>
    public class WebHelper
    {
        /// <summary>
        /// Memcached缓存池名称
        /// </summary>
        public static string MemcachedPoolName
        {
            get { return "Sunstore_Web"; }
        }
        
        /// <summary>
        /// Memcached缓存服务器地址
        /// </summary>
        public static string MemcachedIPServices
        {
            get { return CommonHelper.GetConfigData("MemcachedIPServices"); }
        }

        #region 获取客户端IP
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetAddressIp(bool getLanIP = false)
        {
            if (HttpContext.Current == null)
                return "";

            var request = HttpContext.Current.Request;
            string ipAddress;
            if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
            {
                ipAddress = request.ServerVariables["Remote_Addr"];
            }
            else
            {
                ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            if (ipAddress.IndexOf(",") > 0)
            {
                ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(","));
            }
            return ipAddress;
        }
        #endregion

        /// <summary>
        /// 获取商户签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Intance">当前对象实例</param>
        /// <param name="Encode">是否编码</param>
        /// <returns></returns>
        public static Dictionary<string, string> ObjToDic<T>(T Intance)
        {
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            if (Intance == null)
            {
                return Dic;
            }

            var type = typeof(T);
            var PropertyInfo = type.GetProperties();
            PropertyInfo.AsEnumerable().ToList().ForEach(r =>
            {
                if (r.GetValue(Intance) != null && r.GetValue(Intance).ToString() != "")
                    Dic.Add(r.Name, r.GetValue(Intance).ToString());
            });
             
            return Dic; 
        }
    }
}
