using Newtonsoft.Json;
using Strore_Common.Utility;
using Strore_Common.WxHelper;
using Strore_Data;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonEntity;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Impl
{
    public class WxLoginServices : IWxLoginServices
    {
        /// <summary>
        /// 授权登陆链接
        /// </summary>
        /// <param name="redirect_uri"></param>
        /// <returns></returns>
        public string GetLoginByWxUrl(string redirect_uri)
        {
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
            url = string.Format(url, WxHelper.WxAppid, redirect_uri);
            return url;
        }

        /// <summary>
        /// 验证授权登陆的Code，并获取用户的信息
        /// </summary>
        /// <param name="ValideCode"></param>
        public WxUserEntity ValideWxLoginCode(string ValideCode)
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
            url = string.Format(url, WxHelper.WxAppid, WxHelper.WxAppSecret, ValideCode); 
            var wxcontent = HttpHelper.HttpGet(url); 
            var model= JsonConvert.DeserializeObject<WxLoginDTO>(wxcontent);
            if (model != null)
            {
                var entity = GetUserInfo(model.access_token, model.openid);
                if (entity != null)
                    return entity;
            }
            return new WxUserEntity() { nickname="游客"};
        }

        /// <summary>
        /// 获取用户的信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="useropenid"></param>
        /// <returns></returns>
        private WxUserEntity GetUserInfo(string access_token, string useropenid)
        { 
            string url = " https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
            url = string.Format(url, access_token, useropenid); 
            try
            { 
                var wxcontent = HttpHelper.HttpGet(url); 
                var entity = JsonConvert.DeserializeObject<WxUserEntity>(wxcontent);
                if (entity == null)
                {
                   return null; 
                } 
                if (DbUtility.GetSingle<WxUserEntity>(r => r.openid == entity.openid) == null)
                {
                    DbUtility.Add(entity, true);
                }  
                return entity;
            }
            catch(Exception ex)
            { 
                return null;
            }
        }

    }
}
