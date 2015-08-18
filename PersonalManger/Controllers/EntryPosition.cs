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
        #region 获取录入职务的界面+public ActionResult EntryPosition()
        /// <summary>
        /// 获取录入职务的界面
        /// </summary>
        /// <returns></returns>
        [Common.Attributes.SkipAttribute]
        public ActionResult EntryPosition()
        {
            return CommonView("录入");
        }
        #endregion

        #region 获取删除职务的界面+public ActionResult DelPosition()
        /// <summary>
        /// 获取删除职务的界面
        /// </summary>
        /// <returns></returns>
        [Common.Attributes.SkipAttribute]
        public ActionResult DelPosition()
        {
            return CommonView("删除");
        }
        #endregion

        #region 根据不同的条件获取不同的职位数据+public ActionResult GetEntryData(FormCollection form)
        /// <summary>
        /// 根据不同的条件获取不同的职位数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Common.Attributes.Skip]
        public ActionResult GetEntryData(FormCollection form)
        {
            int pageIndex;
            int role;
            if (!int.TryParse(form["pageindex"], out pageIndex) || !int.TryParse(form["role"], out role))
            {
                return OperateContext.Current.RedirectAjax("err", "录入的职位有错！！！", null, null);
            }
            string oprate = form["oprate"];//业务逻辑上也要进行验证

            if (!Enum.IsDefined(typeof(PositionEx), role))
            {
                return OperateContext.Current.RedirectAjax("err", "录入的职位有错！！！", null, null);
            }
            PositionEx position;
            Enum.TryParse<PositionEx>(role.ToString(), false, out position);

            //验证权限
            if (!Validate(position))
            {
                Uri MyUrl = Request.UrlReferrer;
                string url = MyUrl.ToString();
                return OperateContext.Current.RedirectAjax("nopermission", "您没有权限访问此页面", null, url);
            }

            Expression<Func<MODEL.T_MemberInformation, bool>> filter = null;
            filter = new EntryPositionHelper().EntryDataHelper(position, oprate);
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
                    roles = string.Join(" ", u.T_RoleAct.Where(r => r.IsDel == false).OrderBy(s => s.RoleId).Select(p => p.T_Role.RoleName).ToArray())//效率比较低
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

        #region 将录入的数据保存到数据库中+public ActionResult Entry(FormCollection form)
        /// <summary>
        /// 将录入的数据保存到数据库中
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Common.Attributes.SkipAttribute]
        public ActionResult Entry(FormCollection form)
        {
            int role;
            //客户端验证
            if (!int.TryParse(form["role"], out role))
                return OperateContext.Current.RedirectAjax("err", "录入的职位有错！！！", null, null);

            if (!Enum.IsDefined(typeof(PositionEx), role))
            {
                return OperateContext.Current.RedirectAjax("err", "录入的职位有错！！！", null, null);
            }
            PositionEx position;
            Enum.TryParse<PositionEx>(role.ToString(), false, out position);

            //验证权限
            if (!Validate(position))
            {
                Uri MyUrl = Request.UrlReferrer;
                string url = MyUrl.ToString();
                return OperateContext.Current.RedirectAjax("nopermission", "您没有权限访问此页面", null, url);
            }

            //这里应该验证学号的合法性，暂时不验证
            string stunums = form["stunums"];
            return new EntryPositionHelper().Entry(position, stunums);
        }
        #endregion

        #region 根据担任的不同角色生成不同的录入职位的页面+public ActionResult EntryChoose()
        /// <summary>
        /// 根据担任的不同角色生成不同的录入职位的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EntryChoose()
        {
            //所担任的所有角色
            string Stu = OperateContext.Current.Usr.StuNum;
            List<int> listid = OperateContext.Current.BLLSession.IRoleActBLL.GetListBy(u => u.RoleActor == Stu).Select(p => p.RoleId).ToList();

            List<int> listrole = new List<int>();
            List<int> listdep = new List<int>();
            int dep = 0;
            //总裁可以录入的角色
            if (listid.Contains((int)PositionEx.President))
            {
                listrole = listrole.Concat(GetEntryRoleId(PositionEx.President)).ToList();
            }
            //系统管理员只可以录入总裁
            if (listid.Contains((int)PositionEx.System))
            {
                listrole = listrole.Concat(GetEntryRoleId(PositionEx.System)).ToList();
            }
            //部长可以录入的角色
            if (listid.Contains((int)PositionEx.Minister))
            {
                dep = (int)PositionEx.DepMember;
            }
            listrole = listrole.Distinct().ToList();
            var role = OperateContext.Current.BLLSession.IRoleBLL.GetListBy(u => listrole.Contains(u.RoleId)).ToList();

            ViewBag.Role = role;
            ViewBag.Dep = dep;
            return View();
        }
        #endregion


        /// <summary>
        /// 获取每个角色可以录入的角色Id
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public List<int> GetEntryRoleId(PositionEx position)
        {
            List<int> list = new List<int>();
            switch (position)
            {
                case PositionEx.President:
                    list.Add((int)PositionEx.Minister);
                    list.Add((int)PositionEx.StudyLeader);
                    list.Add((int)PositionEx.StudyMember);
                    list.Add((int)PositionEx.PlanLeader);
                    list.Add((int)PositionEx.PlanMmember);
                    list.Add((int)PositionEx.Financial);
                    break;
                case PositionEx.System:
                    list.Add((int)PositionEx.President);
                    break;
                default:
                    break;
            }
            return list;
        }

        /// <summary>
        /// 获取录入视图共同调用的方法
        /// </summary>
        /// <param name="Oprate"></param>
        /// <returns></returns>
        public ActionResult CommonView(string Oprate)
        {
            ActionResult result = null;
            int Role;
            if (!int.TryParse(Request.QueryString["role"], out Role))
                result = Content("要录入的职位有错误！！！");

            if (!Enum.IsDefined(typeof(PositionEx), Role))
            {
                result = Content("要录入的职位有错误！！！");
            }

            PositionEx position;
            Enum.TryParse<PositionEx>(Role.ToString(), false, out position);

            //验证权限
            if (!Validate(position))
            {
                Uri MyUrl = Request.UrlReferrer;
                string url = MyUrl.ToString();
                result = Content("<script type='text/javascript'>alert('您没有权限访问此页面!');window.location='" + url + "'</script>");
            }
            else
            {
                //部长录入本部门的成员
                string RoleName = "";
                if (position == PositionEx.DepMember)
                {
                    //获取部门编号
                    int? DepId = OperateContext.Current.Usr.Department;
                    RoleName = OperateContext.Current.BLLSession.IDepartmentBLL.GetListBy(u => u.DepartmentId == DepId).Select(p => p.DepartmentName).FirstOrDefault();
                }
                else
                {
                    //根据角色role得到要录入角色的中文名称
                    RoleName = OperateContext.Current.BLLSession.IRoleBLL.GetListBy(u => u.RoleId == Role).Select(p => p.RoleName).FirstOrDefault();
                }
                ViewBag.Oprate = Oprate;
                ViewBag.Role = Role;
                ViewBag.RoleName = RoleName;
                result = View("EntryPosition");
            }
            return result;
        }

        /// <summary>
        /// 由于请求的url没有在数据库配置，无法验证，在这里进行验证
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool Validate(PositionEx role)
        {
            if (role == PositionEx.DepMember)
            {
                List<int> list = OperateContext.Current.BLLSession.IRoleActBLL.GetListBy(u => u.RoleActor == OperateContext.Current.Usr.StuNum).Select(p=>p.RoleId).ToList();
                if (!list.Contains((int)PositionEx.Minister))
                    return false;
            }
            else
            {
                string action = role.ToString();//即为action名称
                if (!OperateContext.Current.HasPemission("PersonalManger", "EntryPosition", action, "post") &&
                    !OperateContext.Current.HasPemission("PersonalManger", "EntryPosition", action, "post"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
