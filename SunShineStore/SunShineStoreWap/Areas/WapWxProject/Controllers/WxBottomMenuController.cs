using Strore_Common.Component;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.WxProject.Controllers
{
    public class WxBottomMenuController : Controller
    {
        //
        // GET: /WxBottomMenu/

        public ActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// 获取底部菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuData()
        {
            var services = LocalServiceLocator.GetService<IWxBottomMenuServices>();
            var result = services.GetMenuData();
            return Json(result);
        }

        /// <summary>
        /// 获取底部菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateMenuData(ReturnMenuDTO DTO)
        {
            var services = LocalServiceLocator.GetService<IWxBottomMenuServices>();
            var result = services.UpdateMenuData(DTO);
            return Json(result);
        }
    }
}
