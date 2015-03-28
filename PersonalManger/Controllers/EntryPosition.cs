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
            Expression<Func<MODEL.T_MemberInformation, bool>> filter = null;
            string oprate = form["oprate"];//获取操作，增加或删除
            /*如果是增加操作*/
            if (oprate == "删除")
            {
                if (role == 10000)//代表删除的是部门成员
                {
                    int DepId = OperateContext.Current.Usr.Department;
                    /*删除实习生*/
                    filter = u => u.IsDelete == false && (u.T_RoleAct.Where(r => r.IsDel == true).Select(p => p.RoleId).Contains(Position.Student));
                }
                if (role == 10010)//代表删除的是技术指导
                {
                    /*当前部长的id*/
                    int DepId = OperateContext.Current.Usr.Department;
                    /*选出这个部门是技术指导的大二成员*/
                    filter = u => u.IsDelete == false && u.Department == DepId && (u.T_RoleAct.Select(p => p.RoleId).Contains(Position.TechnicalGuide)) &&
                        u.StuNum.Contains((DateTime.Now.Year - 2).ToString());
                }
                else
                {
                    filter = u => u.IsDelete == false && (u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(role));
                }
            }
            if (oprate == "录入")
            {
                if (role == 10010)//代表录入的是技术指导
                {
                    int DepId = OperateContext.Current.Usr.Department;
                    filter = u => u.IsDelete == false && u.Department == DepId && (u.T_RoleAct.Select(p => p.RoleId).Contains(Position.TechnicalGuide) == false) &&
    u.StuNum.Contains((DateTime.Now.Year - 2).ToString());
                }
                if (role == 10000)//录入的是部门成员
                {
                    /*选出仍然是实习生的成员*/
                    filter = u => u.IsDelete == false && u.T_RoleAct.Where(r => r.IsDel == false).Select(p => p.RoleId).Contains(Position.Student);
                }
                string datetime = "";
                /*如果录入的是活动策划组，则应该选取大一成员*/
                if (role == Position.PlanLeader || role == Position.PlanMmember)
                {
                    DateTime dt = DateTime.Now;
                    datetime = (dt.Year - 1).ToString();
                }
                else
                {
                    /*其他的则是大二成员*/
                    DateTime dt = DateTime.Now;
                    datetime = (dt.Year - 2).ToString();
                }
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains(role) == false);

            }
            int pageSize = 10;//页容量固定死为10
            int totalRecord;
            var list = OperateContext.Current.BLLSession.IMemberInformationBLL.GetPagedList(pageIndex, pageSize,
                filter, u => u.StuNum, out totalRecord).Select(u => new MemberInformationDTO()
                {
                    StuNum = u.StuNum,
                    StuName = u.StuName,
                    Major = u.Major,
                    TelephoneNumber = u.TelephoneNumber,
                    Department = u.T_Department.DepartmentName,//效率比较低
                    roles = string.Join(" ", u.T_RoleAct.OrderBy(s => s.RoleId).Select(p => p.T_Role.RoleName).ToArray())//效率比较低
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
            return Common(form, "ZC");
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
            return Common(form, "CW");
        }

        public ActionResult EntryTechnicalGuide(FormCollection form)
        {
            return Common(form, "TechnicalGuide");
        }

        #region 录入、删除部门成员+public ActionResult EntryBMCY(FormCollection form)
        /// <summary>
        /// 录入、删除部门成员
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult EntryBMCY(FormCollection form)
        {
            int role = Convert.ToInt32(form["role"]);
            string oprate = form["oprate"];
            string IsPostBack = form["IsPostBack"];
            string stunums = form["stunums"];
            if (string.IsNullOrEmpty(IsPostBack))
            {
                return Content("/PersonalManger/EntryPosition/EntryPosition?role=" + role + "&oprate=" + oprate + "&urlfix=BMCY");
            }
            string[] stum = stunums.Split(new char[] { ';' });
            if (oprate == "录入")
            {
                try
                {
                    int DepId = OperateContext.Current.Usr.Department;
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_MemberInformation member = new MODEL.T_MemberInformation() { StuNum = stum[i], Department = DepId };
                        MODEL.T_RoleAct roleAct = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = Position.Student, IsDel = true };
                        OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, "Department");
                        /*将实习生的身份擦除*/
                        OperateContext.Current.BLLSession.IRoleActBLL.Modify(roleAct, "IsDel");
                    }
                    return OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + role);
                }
                catch (Exception ex)
                {
                    return OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                }
            }
            else//删除部门成员
            {
                try
                {
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_MemberInformation member = new MODEL.T_MemberInformation() { StuNum = stum[i], Department = 10001 };//表设计的有问题，10001代表不属于任何部门
                        OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, "Department");
                        /*删完之后身份变为实习生*/
                        MODEL.T_RoleAct roleAct = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = Position.Student, IsDel = false };
                        OperateContext.Current.BLLSession.IRoleActBLL.Modify(roleAct, "IsDel");
                    }
                    return OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + role);
                }
                catch (Exception ex)
                {
                    return OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
                }
            }
        }
        #endregion

        #region 职务录入共同调用的方法+public ActionResult Common(FormCollection form, string roleStr)
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
            Expression<Func<MODEL.T_RoleAct, bool>> filter = null;
            string[] stum = stunums.Split(new char[] { ';' });
            if (oprate == "录入")
            {
                if (role != 10010)//技术指导不要擦除
                {
                    /*先将原先的职位删除*/
                    string datetime = "";
                    DateTime dt = DateTime.Now;
                    datetime = (dt.Year - 3).ToString();
                    filter = u => u.RoleId == role && (u.RoleActor.Contains(datetime));
                    MODEL.T_RoleAct roleAct = new MODEL.T_RoleAct() { IsDel = true };

                    if (OperateContext.Current.BLLSession.IRoleActBLL.ModifyBy(roleAct, filter, "IsDel") < 0)
                    {
                        return OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                    }
                }
                try
                {
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_RoleAct roleAdd = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = role, IsDel = false };
                        OperateContext.Current.BLLSession.IRoleActBLL.Add(roleAdd);
                    }
                    if (role == 10010)
                        return OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + role);
                    else
                        return OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/BrowsePosition");
                }
                catch (Exception ex)
                {
                    return OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                }
            }
            else//删除职务操作
            {
                try
                {
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_RoleAct roleDel = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = role, IsDel = true };
                        OperateContext.Current.BLLSession.IRoleActBLL.Modify(roleDel, "IsDel");
                    }
                    if (role == 10010)
                        return OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + role);
                    else
                        return OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/PersonalManger/CheckMember/BrowsePosition");
                }
                catch (Exception ex)
                {
                    return OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
                }
            }
        }
        #endregion
    }
}
