﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineStore.Wap.WxProject.Controllers
{
    public class WxHomeController : Controller
    {
        //
        // GET: /WxHome/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetail()
        {
            return View("Detail");
        }

    }
}
