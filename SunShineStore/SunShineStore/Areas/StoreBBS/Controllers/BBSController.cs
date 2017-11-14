using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.BBS.Controllers
{
    public class BBSController : Controller
    {
        //
        // GET: /BBS/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 论坛列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BBSListT()
        {

            return View();
        }

        /// <summary>
        /// 新增帖子
        /// </summary>
        /// <returns></returns>
        public ActionResult BBSWrite()
        {
            return View();
        }
        
    }
}
