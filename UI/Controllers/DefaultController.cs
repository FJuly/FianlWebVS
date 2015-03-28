using MVC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return Content("缺省主页");
        }
    }
}
