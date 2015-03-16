using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
       
        public ActionResult boot()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

    }
}
