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
        #region 根据不同的操作获取不同的数据+ public Expression<Func<MODEL.T_MemberInformation, bool>> EntryDataHelper(PositionEx role, string oprate)
        public Expression<Func<MODEL.T_MemberInformation, bool>> EntryDataHelper(PositionEx role, string oprate)
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

        #region 删除操作时获取得数据+ public Expression<Func<MODEL.T_MemberInformation, bool>> DeleteData(PositionEx role)
        public Expression<Func<MODEL.T_MemberInformation, bool>> DeleteData(PositionEx role)
        {
            Expression<Func<MODEL.T_MemberInformation, bool>> filter = null;
            //部门成员
            if (role == PositionEx.DepMember)
            {
                int? DepId = OperateContext.Current.Usr.Department;
                filter = u => u.IsDelete == false && u.Department == DepId;
            }
            return filter;
        }
        #endregion

        #region 录入操作时获取的数据+public Expression<Func<MODEL.T_MemberInformation, bool>>  EntryData(PositionEx role)
        public Expression<Func<MODEL.T_MemberInformation, bool>> EntryData(PositionEx role)
        {
            Expression<Func<MODEL.T_MemberInformation, bool>> filter = null;

            //部门成员
            if (role == PositionEx.DepMember)
            {
                filter = u => u.IsDelete == false && u.TechnicalLevel == TechnicalLevel.Student;
            }
            //活动策划组，则应该选取大一成员
            if (role == PositionEx.PlanLeader || role == PositionEx.PlanMmember)
            {
                string datetime = "";
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 1).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime));
            }
            //部长
            if (role == PositionEx.Minister)
            {
                string datetime = "";
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 2).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains((int)role) == false);
            }
            //财务主管
            if (role == PositionEx.Financial)
            {
                string datetime = "";
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 2).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains((int)role) == false);
            }
            //总裁
            if (role == PositionEx.President)
            {
                string datetime = "";
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 2).ToString();
                filter = u => u.IsDelete == false && (u.StuNum.Contains(datetime)) && (u.T_RoleAct.Select(p => p.RoleId).Contains((int)role) == false);
            }
            return filter;
        }
        #endregion

        #region 根据不同的操作录入人员+ public ActionResult Entry(PositionEx role, string stunums)
        public ActionResult Entry(PositionEx role, string stunums)
        {
            string[] stum = stunums.Split(new char[] { ';' });
            ActionResult result = null;

            #region 录入部门成员
            if (role == PositionEx.DepMember)
            {
                try
                {
                    int? DepId = OperateContext.Current.Usr.Department;
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_MemberInformation member = new MODEL.T_MemberInformation() { StuNum = stum[i], Department = DepId, TechnicalLevel = TechnicalLevel.FullMember };
                        OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, "Department", "TechnicalLevel");
                    }
                    result = OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + DepId);
                }
                catch (Exception ex)
                {
                    result = OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                }
            }
            #endregion

            #region 录入技术指导
            if (role == PositionEx.TechnicalGuide)
            {
                try
                {
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_RoleAct roleAdd = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = (int)role, IsDel = false };
                        OperateContext.Current.BLLSession.IRoleActBLL.Add(roleAdd);
                    }
                    result = OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + Position.TechnicalGuide);
                }
                catch (Exception ex)
                {
                    result = OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                }
            }
            #endregion

            #region 录入除部门成员和技术指导的其他职位
            if (role != PositionEx.DepMember && role != PositionEx.TechnicalGuide)
            {
                Expression<Func<MODEL.T_RoleAct, bool>> filter = null;
                string date = (DateTime.Now.Year - 3).ToString();
                //将原先这个职位上的大三旧职员删除
                filter = u => u.RoleId == (int)role && u.IsDel == false && u.RoleActor.Contains(date);
                MODEL.T_RoleAct roleAct = new MODEL.T_RoleAct() { IsDel = true };

                if (OperateContext.Current.BLLSession.IRoleActBLL.ModifyBy(roleAct, filter, "IsDel") < 0)
                {
                    result = OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                }
                try
                {
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_RoleAct roleAdd = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = (int)role, IsDel = false };
                        OperateContext.Current.BLLSession.IRoleActBLL.Add(roleAdd);
                    }
                    result = OperateContext.Current.RedirectAjax("ok", "录入成功", null, "/PersonalManger/CheckMember/BrowsePosition");
                }
                catch (Exception ex)
                {
                    result = OperateContext.Current.RedirectAjax("err", "录入失败", null, null);
                }
            }
            #endregion

            return result;
        }
        #endregion

        #region 删除操作+  public ActionResult Delete(PositionEx role, string[] stum)
        public ActionResult Delete(PositionEx role, string[] stum)
        {
            ActionResult result = null;

            #region 删除部门成员
            if (role == PositionEx.DepMember)
            {
                try
                {
                    int? DepId = OperateContext.Current.Usr.Department;
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_MemberInformation member = new MODEL.T_MemberInformation() { StuNum = stum[i], Department = null, TechnicalLevel = TechnicalLevel.Student };
                        OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, "Department", "TechnicalLevel");
                    }
                    result = OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/PersonalManger/CheckMember/DepartmentInfo?DepId=" + DepId);
                }
                catch (Exception ex)
                {
                    result = OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
                }
            }
            #endregion

            #region 删除其他职务
            if (role != PositionEx.DepMember)
            {
                try
                {
                    for (int i = 0; i < stum.Length - 1; i++)
                    {
                        MODEL.T_RoleAct roleDel = new MODEL.T_RoleAct() { RoleActor = stum[i], RoleId = (int)role, IsDel = true };
                        OperateContext.Current.BLLSession.IRoleActBLL.Modify(roleDel, "IsDel");
                    }
                    result = OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/PersonalManger/CheckMember/BrowsePosition");
                }
                catch (Exception ex)
                {
                    result = OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
                }
            }
            #endregion

            return result;
        }
        #endregion
    }
}
