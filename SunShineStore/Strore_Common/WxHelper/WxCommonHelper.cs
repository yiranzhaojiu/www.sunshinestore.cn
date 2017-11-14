using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Xml;

namespace Strore_Common.WxHelper
{
    public class WxCommonHelper
    {
        /// <summary>
        /// 字典排序
        /// </summary>
        /// <param name="parmars"></param>
        /// <returns></returns> 
        public static string DictionaryOrder(params string[] values)
        {
            var arry = values.ToArray();
            Array.Sort(arry);
            return string.Join("", arry);
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <returns></returns>
        public static string EncryptShaOne(string tmpStr)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptSHA1Replace(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
         

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetUnixTime()
        {
            return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString(); 
        }

        /// <summary>
        /// 随机字符串，不长于32位。推荐随机数生成算法
        /// </summary>
        /// <returns></returns>
        public static string GetNewGuidStr()
        {
           return Guid.NewGuid().ToString().Replace("-", ""); 
        }

        /// <summary>
        /// 获取商户签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Intance">当前对象实例</param>
        /// <param name="Encode">是否编码</param>
        /// <returns></returns>
        public static string GetPaySignStr<T>(T Intance, bool Encode = false)
        {
            if (Intance == null)
            {
                return string.Empty;
            }

            Dictionary<string, string> Dic = new Dictionary<string, string>();

            var type = typeof(T);
            var PropertyInfo = type.GetProperties();

            #region
            //if (r.PropertyType == typeof(string))
            //{
            //    if (string.isnullorempty(r.getvalue(type).tostring()))
            //    {
            //        dic.add(r.getmethod.name, r.getvalue(type).tostring());
            //    }
            //}
            #endregion 
            PropertyInfo.AsEnumerable().ToList().ForEach(r =>
            {
                if(r.GetValue(Intance)!=null&& r.GetValue(Intance).ToString()!="")
                    Dic.Add(r.Name, r.GetValue(Intance).ToString());
            });
 
            if (Dic.Count <= 0)
                return string.Empty;

            var result = from p in Dic
                         where p.Value != null && p.Value != "" && p.Value != "0"
                         orderby p.Key
                         select p;

            var buff = string.Empty; 
            result.AsEnumerable().ToList().ForEach(r=> {
                if (r.Key != "")
                {
                    buff += r.Key + "=" + (Encode ? System.Web.HttpUtility.UrlEncode(r.Value) : r.Value) + "&";
                }
            }); 
            return buff;
        }

        /// <summary>
        /// 获取商户签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Dic">要签名的集合</param>
        /// <returns></returns>
        public static string GetPaySignStr(Dictionary<string,string> Dic)
        { 
            if (Dic.Count <= 0)
                return string.Empty;

            var result = from p in Dic
                         where p.Value != null && p.Value != "" && p.Value != "0"
                         orderby p.Key
                         select p;

            var buff = string.Empty;
            result.AsEnumerable().ToList().ForEach(r => {
                if (r.Key != "")
                {
                    buff += r.Key + "=" + r.Value+ "&";
                }
            });
            return buff;
        }

        /// <summary>
        /// 验证字符号是否可以转换成INT类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(String str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 封装Dictionary TO XML
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToXml(Dictionary<string, string> arr)
        {
            String xml = "<xml>";

            foreach (KeyValuePair<string, string> pair in arr)
            {
                String key = pair.Key;
                String val = pair.Value;
                if (IsNumeric(val))
                {
                    xml += "<" + key + ">" + val + "</" + key + ">";

                }
                else
                    xml += "<" + key + "><![CDATA[" + val + "]]></" + key + ">";
            }

            xml += "</xml>";
            return xml;
        }

        /// <summary>
        /// String转换成Dictionary
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Dictionary<string, string> StrToDic(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {

                throw new ArgumentNullException("要转换的字符串不存在");
            } 
           var m_values = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes; 
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }
             
            return m_values;
        } 
    }
}
