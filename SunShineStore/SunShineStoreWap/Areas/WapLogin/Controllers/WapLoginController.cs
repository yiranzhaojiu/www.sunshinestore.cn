using Strore_Common.Component;
using SunShineStore.Customer.Interface;
using SunShineStore.CustomerDTO;
using SunShineStore.WxCommonEntity;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.Customer.Controllers
{
    /// <summary>
    /// WAP 用户登录
    /// </summary>
    public class WapLoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            ViewBag.VID =string.Concat(Guid.NewGuid().ToString().Replace("-","").ToUpper(),DateTime.Now.ToString("yyyyMMddHHmmss"));
            return View();
        }

        [HttpPost]
        public ActionResult Login(CusetomerUserInfoDTO dto)
        {
            var services = LocalServiceLocator.GetService<ICustomerInfoServices>();
            var result= services.ValidUserInfo(dto);
            return Json(result);
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult WxLogin(string code = "", string state = "")
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            {
                string redirect_uri = HttpContext.Request.Url.ToString();
                var services = LocalServiceLocator.GetService<IWxLoginServices>();
                var RedictUrl = services.GetLoginByWxUrl(Server.UrlEncode(redirect_uri));

                if (string.IsNullOrEmpty(RedictUrl))
                {
                    return Content("系统错误，刷新页面重试");
                }
                return Redirect(RedictUrl);
            }
            var model = new WxUserEntity();
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(state))
            {
                var services = LocalServiceLocator.GetService<IWxLoginServices>();
                model=services.ValideWxLoginCode(code);
            }

            return View(model);

        }
    }
}
