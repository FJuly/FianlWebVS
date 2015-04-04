using MODEL.DTO;
using MODEL.ViewModel;
using MVC.Helper;
using P01MVCAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PersonalManger
{
    public class EntryPositionController : Controller
    {

        public ActionResult EntryChoose()
        {
            return View();
        }

        #region 获取录入职务的界面+public ActionResult EntryPosition(FormCollection form)
        /// <summary>
        /// 获取录入职务的界面
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult EntryPosition(FormCollection form)
        {
            string num = Request.QueryString["role"];
            string oprate = Request.QueryString["oprate"];
            string urlfix = Request.QueryString["urlfix"];
            if (oprate == "Add")
                ViewBag.oprate = "录入";
            else
                ViewBag.oprate = "删除";
            ViewBag.num = num;
            ViewBag.urlfix = urlfix;
            return View();
        }
        #endregion

        #region 根据不同的条件获取不同的职位数据+public ActionResult GetEntryData(FormCollection form)
        /// <summary>
        /// 根据不同的条件获取不同的职位数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult GetEntryData(FormCollection form)
        {
            int pageIndex = Convert.ToInt32(form["pageindex"]);
            int role = Convert.ToInt32(form["role"]);
            string oprate = form["oprate"];//获取操作，增加或删除
            Expression<Func<MODEL.T_MemberInformation, bool>> filter = null;

            filter = new EntryPositionHelper().EntryDataHelper(role,oprate);
            int pageSize = 10;//页容量固定死为10
            int totalRecord;
            var list = OperateContext.Current.BLLSession.IMemberInformationBLL.GetPagedList(pageIndex, pageSize,
                filter, u => u.StuNum, out totalRecord).Select(u => new MemberInformationDTO()
                {
                    StuNum = u.StuNum,
                    StuName = u.StuName,
                    Major = u.Major,
                    TelephoneNumber = u.TelephoneNumber,
                    Department = u.T_Department == null ? "无" : u.T_Department.DepartmentName,
                    roles = string.Join(" ", u.T_RoleAct.Where(r=>r.IsDel==false).OrderBy(s => s.RoleId).Select(p => p.T_Role.RoleName).ToArray())//效率比较低
                });
            totalRecord = list.Count();
            PageModel pageModel = new PageModel()
            {
                TotalRecord = totalRecord,
                data = list
            };

            JsonModel json = new JsonModel()
            {
                Data = pageModel,
                BackUrl = "",
                Statu = "ok",
                Msg = "成功"
            };

            JsonResult jr = new JsonResult();
            jr.Data = json;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }
        #endregion


        public ActionResult EntryZC(FormCollection form)
        {
            ActionResult r = Common(form, "ZC");
            return r;
        }

        public ActionResult EntryBZ(FormCollection form)
        {
            return Common(form, "BZ");
        }

        public ActionResult EntryTZ(FormCollection form)
        {
            return Common(form, "TZ");
        }

        public ActionResult EntryTY(FormCollection form)
        {

            return Common(form, "TY");
        }

        public ActionResult EntryZZ(FormCollection form)
        {
            return Common(form, "ZZ");
        }

        public ActionResult EntryZY(FormCollection form)
        {
            return Common(form, "ZY");
        }

        public ActionResult EntryCW(FormCollection form)
        {
            ActionResult r= Common(form, "CW");
            return r;
        }

        //public ActionResult EntryTechnicalGuide(FormCollection form)
        //{
        //    return Common(form, "TechnicalGuide");
        //}

        #region 录入、删除部门成员+public ActionResult EntryBMCY(FormCollection form)
        /// <summary>
        /// 录入、删除部门成员
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult EntryBMCY(FormCollection form)
        {
            return Common(form, "BMCY");
        }
        #endregion

        #region 共同调用的方法+public ActionResult Common(FormCollection form, string roleStr)
        /// <summary>
        /// 职务录入共同调用的方法
        /// </summary>
        /// <param name="form"></param>
        /// <param name="roleStr"></param>
        /// <returns></returns>
        public ActionResult Common(FormCollection form, string roleStr)
        {
            int role = Convert.ToInt32(form["role"]);
            string oprate = form["oprate"];
            string IsPostBack = form["IsPostBack"];
            string stunums = form["stunums"];
            /*当在选择页面选择录入或者的删除职位的时候的为第一次请求*/
            if (string.IsNullOrEmpty(IsPostBack))
            {
                return Content("/PersonalManger/EntryPosition/EntryPosition?role=" + role + "&oprate=" + oprate + "&urlfix=" + roleStr);
            }
             return new EntryPositionHelper().EntryOrDelete(role, oprate, stunums);
        }
        #endregion
    }
}
