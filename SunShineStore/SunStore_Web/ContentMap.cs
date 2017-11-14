using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SunStore_Web
{
    /// <summary>
    /// 静态资源获取路径类
    /// </summary>
    public static class ContentMap
    {
        /// <summary>
        /// 静态资源网址
        /// </summary>
        private static string staticSourceUrl = string.Empty;
        /// <summary>
        /// 版本号
        /// </summary>
        private static string fileVersion = string.Empty;

        /// <summary>
        /// 是否是测试环境
        /// </summary>
        private static string IsTest = string.Empty;

        static ContentMap()
        { 
            staticSourceUrl = System.Configuration.ConfigurationManager.AppSettings["StaticUrl"];
            fileVersion = System.Configuration.ConfigurationManager.AppSettings["FileVersion"];
            IsTest = System.Configuration.ConfigurationManager.AppSettings["IsTest"];
        }


        /// <summary>
        /// 获取静态资源路径路径
        /// </summary>
        /// <param name="context"></param>
        /// <param name="root"></param>
        /// <returns></returns>
  
        public static string GetStaticSourceUrl(string path)
        {
            if (IsTest.Equals("1"))
            {
                return "/" + path;
            }

            if (!staticSourceUrl.EndsWith("/"))
            {
                staticSourceUrl = staticSourceUrl + "/";
            } 

            return staticSourceUrl + path + "?v=" + fileVersion; 
        } 
    }
}
