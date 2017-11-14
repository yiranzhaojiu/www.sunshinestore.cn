using Newtonsoft.Json;
using Strore_Common.Cache;
using Strore_Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Strore_Common.WxHelper
{
    /// <summary>
    /// 微信帮助类
    /// </summary>
    public class WxHelper
    { 
        private static string WxAccesstokenCacheKey = "SunShineStoreWxAccessTokenCacheKey";

        private static readonly object lockobj = new object();

        /// <summary>
        ///  APPID
        /// </summary>
        public static string WxAppid
        {
            get { return CommonHelper.GetConfigData("WxAppID"); } 
        }
         
        /// <summary>
        /// AppSecret秘钥
        /// </summary>
        public static string WxAppSecret
        {
            get { return CommonHelper.GetConfigData("WxAppSecret"); }
        }

        /// <summary>
        /// WxAPIPaySecret秘钥
        /// </summary>
        public static string WxAPIPaySecret
        {
            get { return CommonHelper.GetConfigData("WxAPIPaySecret"); }
        }
         
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public static string mch_id
        {
            get {   return CommonHelper.GetConfigData("mch_id");}
        }
         
        /// <summary>
        /// 获取AccessToken值
        /// </summary>
        public static string WxAccessToken
        {
            get
            {
                if (CacheManager.KeyExists(WxAccesstokenCacheKey))
                {
                    return CacheManager.Get(WxAccesstokenCacheKey).ToString();
                }

                string AccessToken = string.Empty;
                lock (lockobj)
                {
                    AccessToken = OperatorAccessTokenInfo();
                    CacheManager.Set(WxAccesstokenCacheKey, AccessToken, DateTime.Now.AddMinutes(100));
                }
                return AccessToken;
            }
        }

        /// <summary>
        /// 调用接口
        /// </summary>
        /// <returns></returns>
        private static string OperatorAccessTokenInfo()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", WxHelper.WxAppid, WxHelper.WxAppSecret);
            var content = HttpHelper.HttpGet(url);

            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException("获取微信AccessToken失败");

            try
            {
                var jObject = Newtonsoft.Json.Linq.JObject.Parse(content);
                if (!string.IsNullOrEmpty(jObject["access_token"].ToString()))
                {
                    return jObject["access_token"].ToString();
                }
                else
                {
                    throw new ArgumentNullException("获取微信AccessToken切割失败");
                }
            }
            catch{
                throw new ArgumentNullException("获取微信AccessToken切割失败");
            } 
         
        }
         
    }
}
