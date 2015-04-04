using MVC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PersonalManger
{
    public class EntryPositionHelper
    {
        #region 根据不同的操作获取不同的数据+ public Expression<Func<MODEL.T_MemberInformation, bool>> EntryDataHelper(int role, string oprate)
        public Expression<Func<MODEL.T_MemberInformation, bool>> EntryDataHelper(int role, string oprate)
        {
            /*如果是增加操作*/
            if (oprate == "删除")
            {
                return DeleteData(role);
            }
            if (oprate == "录入")
            {
                return EntryData(role);
            }
            return null;
        }
        #endregion

        #region 删除操作时获取得瑟数据+ public Expression<Func<MODEL.T_MemberInformation, bool>> EntryData(int role)
        public Expression<Func<MODEL.T_MemberInformation, bool>> DeleteData(int role)
        {
            Expression<Func<MODEL.T_MemberInformation, bool>> filter = null;
            /*代表删除的是部门成员*/
            if (role == 10000)
            {
                int? DepId = OperateContext.Current.Usr.Department;
                filter = u => u.IsDelete == false && u.Department == DepId;
            }
            if (role == 10010)//代表删除的是技术指导
            {
                /*当前部长的id*/
                int? DepId = OperateContext.Current.Usr.Department;
                /*选出这个部门是技术指导的大二成员*/
                filter = u => u.IsDelete == false && u.Department == DepId && (u.T_RoleAct.Select(p => p.RoleId).Contains(Position.TechnicalGuide)) &&
                    u.StuNum.Contains((DateTime.Now.Year - 2).ToString());
            }
            else
            {
                filter = u => u.IsDelete == false && (u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(role));
            }
            return filter;
        }
        #endregion

        #region 录入操作时获取的数据+public Expression<Func<MODEL.T_MemberInformation, bool>> DeleteData(int role)
        public Expression<Func<MODEL.T_MemberInformation, bool>> EntryData(int role)
        {
            Expression<Func<MODEL.T_MemberInformation, bool>> filter = null;

            /*带代表录入的是技术指导*/
            if (role == 10010)
            {
                int? DepId = OperateContext.Current.Usr.Department;
                filter = u => u.IsDelete == false && u.Department == DepId && (u.T_RoleAct.Select(p => p.RoleId).Contains(Position.TechnicalGuide) == false) &&
u.StuNum.Contains((DateTime.Now.Year - 2).ToString());
            }
            /*代表录入的是部门成员*/
            if (role == 10000)
            {
                filter = u => u.IsDelete == false && u.TechnicalLevel == TechnicalLevel.Student;
            }
            string datetime = "";
            /*如果录入的是活动策划组，则应该选取大一成员*/
            if (role == Position.PlanLeader || role == Position.PlanMmember)
            {
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 1).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime));
            }
            if (role==Position.Minister)
            {
                /*部长*/
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 2).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains(role) == false);
            }
            if (role == Position.Financial)
            {
                /*财务主管*/
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 2).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains(role) == false);
            }
            if (role == Position.President )
            {
                /*总裁*/
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 2).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains(role) == false);
            }
            if (role == Position.TechnicalGuide)
            {
                /*技术指导*/
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 2).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains(role) == false);
            }
            return filter;
        }
        #endregion



        #region 根据不同的操作录入人员+public ActionResult EntryOrDelete(int role, string oprate, string stunums)
        public ActionResult EntryOrDelete(int role, string oprate, string stunums)
        {
            string[] stum = stunums.Split(new char[] { ';' });
            if (oprate == "录入")
            {
                return EntryOperate(role, stum);
            }
            if (oprate == "删除")
            {
                return DeleteOperate(role, stum);
            }
            return null;
        }
        #endregion

        #region 录入操作+ public ActionResult EntryOperate(int role, string[] stum)
        public ActionResult EntryOperate(int role, string[] stum)
        {
            if (role == 10000)
            {
                return EntryDpartment(stum, role);
            }
            else
            {
                return EntryPosition(stum, role);
            }
        }
        #endregion

        #region 删除操作+ public ActionResult DeleteOperate(int role, string[] stum)
        public ActionResult DeleteOperate(int role, string[] stum)
        {
            if (role == 10010)
                return DeleteDpartment(role, stum);
            else
                return DeletePosition(role, stum);
        }
        #endregion

        #region 录入职务+public ActionResult EntryPosition(string[] stum, int role)
        public ActionResult EntryPosition(string[] stum, int role)
        {
            Expression<Func<MODEL.T_RoleAct, bool>> filter = null;
            string date = (DateTime.Now.Year - 3).ToString();
                /*将原先这个职位上的大三旧职员删除*/
            filter = u => u.RoleId == role && u.IsDel == false && u.RoleActor.Contains(date);
                MODEL.T_RoleAct roleAct = new MODEL.T_RoleAct() { IsDel = true };

                if (OperateContext.Current.BLLSession.IRoleActBLL.ModifyBy(roleAct, filter, "IsDel") < 0)
                {
                    return OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                }
            try
            {
                for (int i = 0; i < stum.Length - 1; i++)
                {
                    MODEL.T_RoleAct roleAdd = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = role, IsDel = false };
                    OperateContext.Current.BLLSession.IRoleActBLL.Add(roleAdd);
                }
                    return OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/BrowsePosition");
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
            }
        }
        #endregion

        #region 录入技术指导+ public ActionResult EntryTechnicalGuide(string[] stum, int role)
        public ActionResult EntryTechnicalGuide(string[] stum, int role)
        {
            try
            {
                for (int i = 0; i < stum.Length - 1; i++)
                {
                    MODEL.T_RoleAct roleAdd = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = role, IsDel = false };
                    OperateContext.Current.BLLSession.IRoleActBLL.Add(roleAdd);
                }
                    return OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + Position.TechnicalGuide);
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
            }
        }
        #endregion

        #region 录入部门成员+ public ActionResult EntryDpartment(string[] stum, int role)
        public ActionResult EntryDpartment(string[] stum, int role)
        {
            try
            {
                int? DepId = OperateContext.Current.Usr.Department;
                for (int i = 0; i < stum.Length - 1; i++)
                {
                    MODEL.T_MemberInformation member = new MODEL.T_MemberInformation() { StuNum = stum[i], Department = DepId, TechnicalLevel = TechnicalLevel.FullMember };
                    OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, "Department", "TechnicalLevel");
                }
                return OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + DepId);
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
            }
        }
        #endregion

        #region 删除职务+public ActionResult DeletePosition(int role, string[] stum)
        public ActionResult DeletePosition(int role, string[] stum)
        {
            try
            {
                for (int i = 0; i < stum.Length - 1; i++)
                {
                    MODEL.T_RoleAct roleDel = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = role, IsDel = true };
                    OperateContext.Current.BLLSession.IRoleActBLL.Modify(roleDel, "IsDel");
                }
                    return OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/PersonalManger/CheckMember/BrowsePosition");
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
            }
        }
        #endregion

        #region 删除部门成员+public ActionResult DeleteDpartment(int role, string[] stum)
        public ActionResult DeleteDpartment(int role, string[] stum)
        {
            try
            {
                int? DepId = OperateContext.Current.Usr.Department;
                for (int i = 0; i < stum.Length - 1; i++)
                {
                    MODEL.T_MemberInformation member = new MODEL.T_MemberInformation() { StuNum = stum[i], Department = null, TechnicalLevel = TechnicalLevel.Student };
                    OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, "Department", "TechnicalLevel");
                }
                return OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + DepId);
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
            }
        }
        #endregion
    }
}
