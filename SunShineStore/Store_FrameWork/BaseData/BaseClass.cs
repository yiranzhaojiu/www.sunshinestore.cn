using Strore_Common.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_FrameWork.BaseData
{
    public class BaseClass
    {
        /// <summary>
        /// 获取数据库的配置文件路径
        /// </summary>
        static string xmlPath = CommonHelper.GetConfigData("StoreDbConfig", "");

        /// <summary>
        /// 连接串哈希表
        /// </summary>
        static Hashtable ConnectionStringHS = new Hashtable();

        private static readonly object lockObj = new object();

        /// <summary>
        /// 获取数据库的连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string  GetConnenctionStr(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            //读取XML文件，获取数据库的配置字符串
            if (string.IsNullOrEmpty(xmlPath))
                return string.Empty;

            string xmlkey = string.Empty;
            switch (key)
            {
                case "BusinessMgRead":
                    xmlkey = "MgRead";
                    break;
                case "BusinessMgWrite":
                    xmlkey = "MgWreite";
                    break;
                case "UserInfoRead":
                    xmlkey = "UserInfoRead";
                    break;
                case "UserInfoWrite":
                    xmlkey = "UserInfoWrite";
                    break;
                default:
                    xmlkey = string.Empty;
                    break;
            } 

            if (string.IsNullOrEmpty(xmlkey)) 
                return string.Empty;

             
            DataSet ds = new DataSet(); 
            ds.ReadXml(xmlPath);
            DataTable dt = ds.Tables["database"];

            var connstr = string.Empty;
            if (dt.Columns.IndexOf(xmlkey) > -1)
                connstr = Convert.ToString(dt.Rows[0][xmlkey]);

            if (string.IsNullOrEmpty(connstr))
                return string.Empty;

            //加入到全局的静态变量中
            lock (lockObj)
            {
                //if (!ConnectionStringHS.ContainsKey(""))
                //{
                //    ConnectionStringHS.Add();
                //}
            }
            return connstr;
        }
    }
}
