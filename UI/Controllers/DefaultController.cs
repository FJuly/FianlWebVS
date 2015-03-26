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
        //
        // GET: /Default/

        public ActionResult boot()
        {
            return View();
        }

        public ActionResult Index()
        {
            //new DALMSSQL.DBSessionFactory();
            MODEL.T_MemberInformation member = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.StuNum == "201258080133").FirstOrDefault();
            ViewBag.member = member;
            string RoleString = "";
            /*获取该人担任的职务*/
            List<string> role = member.T_RoleAct.Select(u => u.T_Role.RoleName).ToList();
            for (int i = 0; i < role.Count; i++)
            {
                if (i > 2)
                {
                    RoleString = RoleString + "...";
                    break;
                }

                RoleString = RoleString + " " + role[i];
            }
            ViewBag.RoleString = RoleString;
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

    }
}
