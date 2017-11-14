using Strore_Common.Component;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonService.Interface;
using SunShineStoreApi.Filters.OtherPrj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;


namespace SunShineStoreApi.Controllers.WxProject
{
    [OtherPrjAuthorFilter]

    public class WxServerConfigController : ApiController
    {
        //
        // GET: /WxServerConfig/

        /// <summary>
        /// 微信服务器配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage WxServiceConfig([FromUri]WxServiceConfigDTO model)
        {
            string msg = "error"; 
            if (model != null && !string.IsNullOrEmpty(model.echostr) && !string.IsNullOrEmpty(model.nonce) && !string.IsNullOrEmpty(model.signature) && !string.IsNullOrEmpty(model.timestamp))
            {
                var services = LocalServiceLocator.GetService<IWxServerConfigServices>();
                if (services.WxUrlValidate(model))
                    msg=model.echostr;
            } 
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(msg, Encoding.UTF8, "text/plain")
            };  
        } 
    }
}
