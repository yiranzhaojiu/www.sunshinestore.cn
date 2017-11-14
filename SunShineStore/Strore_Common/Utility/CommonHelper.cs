using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Utility
{
    public class CommonHelper
    {
        static ConcurrentDictionary<string, string> configDictionary = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 取出配置文件
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static string GetConfigData(string xmlstr)
        {
            return configDictionary.GetOrAdd(xmlstr, ConfigurationManager.AppSettings[xmlstr]);
        }

        /// <summary>
        /// 取出配置文件(带默认值)
        /// </summary>
        /// <param name="xmlstr">节点名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetConfigData(string xmlstr, string defaultValue = "")
        {
            var configStr = configDictionary.GetOrAdd(xmlstr, ConfigurationManager.AppSettings[xmlstr]);
            return string.IsNullOrEmpty(configStr) ? defaultValue : configStr;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t) where T : class
        {
            return JsonConvert.SerializeObject(t); 
        }

        #region object对象转为int, 失败返回0
        /// <summary>
        /// object对象转为int, 失败返回0
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int ObjectToInt(object o)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(o);
            }
            catch
            {
                i = 0;
            }
            return i;
        }
        #endregion
    }
}
