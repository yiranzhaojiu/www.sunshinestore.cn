using Strore_Common.WebHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.Customer.Controllers
{
    public class ImgController : Controller
    { 
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>    
        public ActionResult Index(string vid)
        { 
            if (string.IsNullOrEmpty(vid))
            {
                return new EmptyResult();
            }
            var imageData = ImgHelper.BulidVerifyCode(ValidateCodeHelper.GetCalculationCode(vid)); 
            return new FileStreamResult(new MemoryStream(imageData), "image/Gif");
        }

    }
}
