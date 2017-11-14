using Strore_Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Strore_Common.Extensions;
using SunShineStoreApi.Common;

namespace SunShineStoreApi.Filters.OtherPrj
{
    public class OtherPrjAuthorFilter : ActionFilterAttribute 
    {
        private static string OtherPrjKey = CommonHelper.GetConfigData("OtherPrjKey", "1R47fEgL");

        /// <summary>
        /// 行为发生之前
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //获取签名
            var verifyCode =  actionContext.Request.GetValue("verifyCode");
            var type = actionContext.ActionDescriptor.ReturnType;
            if (string.IsNullOrEmpty(verifyCode))
            {
                actionContext.Response = ObjectContentBuilder.Create(type, -99, "签名验证失败" + verifyCode, actionContext.Request);
                return;
            }

            //生成签名
            var time = DateTime.Now.ToString("yyyyMMdd");
            string strKey = time + "_" + OtherPrjKey;
            var  _verifyCode=EncryptHelper.MD5Encrypt(strKey); 
            //验证签名
            if (string.Compare(_verifyCode,verifyCode,true)!=0)
            {
                var content = "签名验证失败";
                if (CommonHelper.GetConfigData("IsTest").Equals("1")) 
                    content +=_verifyCode;  

                actionContext.Response = ObjectContentBuilder.Create(type, -99, content, actionContext.Request);
                return;
            } 
        } 

        /// <summary>
        /// 行为发生之后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        
        }
        
    }
}