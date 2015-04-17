using MODEL.DTO;
using MODEL.ViewModel;
using MVC.Helper;
using P01MVCAjax.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalManger
{
    public class CheckMemberController : Controller
    {
        #region 人信息展示页面+ public ViewResult  ()
        public ViewResult PersonPage()
        {
            string stunum =Request.QueryString["StuNum"];
            //判断查看的是不是自己的信息
            if (stunum == null)
            {
                 stunum = OperateContext.Current.Usr.StuNum;
            }
            /*判断每个人是否具有修改的权限*/
            string IsEdit;
            if (OperateContext.Current.HasPemission("PersonalManger", "CheckMember", "AdminEdit", "post"))
            {
                IsEdit = "True";
                ViewBag.IsEdit = IsEdit;
            }
            else
            {
                IsEdit = "False";
                ViewBag.IsEdit = IsEdit;
            }
            MODEL.T_MemberInformation member = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.StuNum == stunum).FirstOrDefault();
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
        #endregion

        #region 返回成员信息查看列表页面+public ActionResult Index()
        /// <summary>
        /// 返回成员信息查看列表页面
        /// </summary>+
        /// <returns></returns>
        public ActionResult Index()
        {
            /*判断每个人是否具有修改的权限*/
            string IsEdit;
            if (OperateContext.Current.HasPemission("PersonalManger", "CheckMember", "AdminEdit", "post"))
            {
                IsEdit = "True";
                ViewBag.IsEdit = IsEdit;
            }
            else
            {
                IsEdit = "False";
                ViewBag.IsEdit = IsEdit;
            }
            return View();
        }
        #endregion

        #region 分页获取数据+public ActionResult GetPageData(FormCollection form)
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Common.Attributes.Skip]
        public ActionResult GetPageData(FormCollection form)
        {
            string dataBy = form["dataBy"];//模糊查寻的条件
            int pageIndex = Convert.ToInt32(form["pageindex"]);
            Expression<Func<MODEL.T_MemberInformation, bool>> whereLambda;
            try
            {
                //根据dataBy生成不同的查寻条件
                if (!string.IsNullOrEmpty(dataBy))
                {
                    whereLambda = u => u.IsDelete == false && (u.StuNum.Contains(dataBy) || u.StuName.Contains(dataBy));
                }
                else
                {
                    whereLambda = u => u.IsDelete == false;
                }
                return PageData(whereLambda, pageIndex);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 根据id删除成员
        /// <summary>
        /// 根据id删除成员
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult DeleteBy(FormCollection form)
        {
            string stuNum = form["stuNum"];
            MODEL.T_MemberInformation model = new MODEL.T_MemberInformation();
            model.IsDelete = true;
            int delCount = OperateContext.Current.BLLSession.IMemberInformationBLL.ModifyBy(model, u => u.StuNum == stuNum, "IsDelete");
            if (delCount > 0)
            {
                return OperateContext.Current.RedirectAjax("ok", "删除成功", null, null);
            }
            else
            {
                return OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
            }
        }
        #endregion

        #region 获取修改主页上的所有数据+public ViewResult PageEdit()
        /// <summary>
        /// 获取修改主页上的所有数据
        /// </summary>
        /// <returns></returns>
        [Common.Attributes.Skip]
        public ViewResult PageEdit()
        {
            /*准备好数据*/
            string stunum = Request["StuNum"];
            string datetime = (Convert.ToInt32(stunum.Substring(0,4))-1).ToString();//成员的技术指导和学习顾问都应该是比自己大一届的成员
            List<MODEL.T_Department> dep = OperateContext.Current.BLLSession.IDepartmentBLL.GetListBy(u => u.DepartmentId > 0);
            List<MODEL.T_TechnicaLevel> techLeval = OperateContext.Current.BLLSession.ITechnicaLevelBLL.GetListBy(u => u.TechLevelId > 0);
            /*chengyuan */
            List<MODEL.T_MemberInformation> StudyGuide = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.StuNum.Contains(datetime) && (u.T_RoleAct.Select(p => p.RoleId).Contains(Position.StudyMember) || u.T_RoleAct.Select(p => p.RoleId).Contains(Position.StudyLeader)));
            //List<MODEL.T_Organization> organization = OperateContext.Current.BLLSession.IOrganizationBLL.GetListBy(u => u.OrganizationId > 0);
            MODEL.T_MemberInformation member = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.StuNum == stunum).FirstOrDefault();
            if (OperateContext.Current.HasPemission("PersonalManger", "CheckMember", "AdminEdit", "3"))
            {
                ViewBag.HasPer = true;
                ViewBag.urlfix = "AdminEdit";
            }
            else
            {
                ViewBag.urlfix = "Edit";
            }
            ViewBag.dep = dep;
            ViewBag.techLeval = techLeval;
            ViewBag.StudyGuide = StudyGuide;
            ViewBag.member = member;
            //ViewBag.organization = organization;
            return View();
        }
        #endregion

        #region  管理员修改方法，可以修改所有信息+public ActionResult AdminEdit(MODEL.T_MemberInformation member)
        /// <summary>
        /// 管理员修改方法，可以修改所有信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ActionResult AdminEdit(MODEL.T_MemberInformation member)
        {
            if (ModelState.IsValid)
            {
                /*EF修改主键一定要加*/
                string[] proNames = new string[] { "StuNum", "StuName", "Gender", "Email", "LoginPwd", "Class", "Major", "Counselor", "HeadTeacher", "UndergraduateTutor", "TelephoneNumber",
            "HomPhoneNumber","FamilyAddress","Department","TechnicalLevel","StudyGuideNumber","TechnicalGuideNumber","Organization","Sign","OtheInfor"};
                int IsSuccess = OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, proNames);
                if (IsSuccess > 0)
                    return Content("<script>alert('修改成功');window.location='/PersonalManger/CheckMember/PersonPage?StuNum=" + member.StuNum+ "</script>");
                else 
                {
                    return Content("<script>alert('修改失败');window.location='/PersonalManger/CheckMember/PageEdit?StuNum=" + member.StuNum + "</script>");
                }
            }
            else
            {
                return Content("修改失败");
            }

        }
        #endregion

        #region 普通成员编辑的自己的信息+public ActionResult Edit(MODEL.T_MemberInformation member)
        /// <summary>
        /// 普通成员编辑的自己的信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [Common.Attributes.Skip]
        public ActionResult Edit(MODEL.T_MemberInformation member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*EF修改主键一定要加*/
                    string[] proNames = new string[] { "StuNum", "StuName", "Gender", "Email", "LoginPwd", "Class", "Major", "Counselor", "HeadTeacher", "UndergraduateTutor", "TelephoneNumber",
            "HomPhoneNumber","FamilyAddress","Sign","OtheInfor"};
                    OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, proNames);
                    return Content("<script>alert('修改成功');window.location='/PersonalManger/CheckMember/PersonPage?StuNum=" + member.StuNum + "'</script>");
                }
                else
                {
                    return Content("<script>alert('修改成功');window.location='/PersonalManger/CheckMember/PageEdit?StuNum=" + member.StuNum + "'</script>");
                }
            }
            catch (Exception ex)
            {
                return Content("<script>alert('修改成功');window.location='/PersonalManger/CheckMember/PageEdit?StuNum=" + member.StuNum + "'</script>");
            }
        } 
        #endregion

        #region 上传头像+public ActionResult UpLoadImg(HttpPostedFileBase UpLoadImg)
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="UpLoadImg"></param>
        /// <returns></returns>
        public ActionResult UpLoadImg()
        {
            HttpPostedFileBase head=Request.Files["head"];
            string StuNum=Request["StuNum"];
            string fileName = head.FileName;
            string ext = Path.GetExtension(fileName).ToLower();
            if (!ext.Equals(".gif") && !ext.Equals(".jpg") && !ext.Equals(".png") && !ext.Equals(".bmp"))
            {

                return Content("<script>alert('您上传的文件格式不正确！上传格式有(.gif、.jpg、.png、.bmp)')</script>");
            }
            else if (head.ContentLength > 1048576 * 5)
            {
                return Content("<script>alert('内容最大为5M')</script>");
            }
            else
            {
                try
                {
                    head.SaveAs(Server.MapPath("~/HeadImg/" + fileName));
                    string ImagePath = "../../HeadImg/" + fileName;
                    MODEL.T_MemberInformation user = new MODEL.T_MemberInformation() { StuNum = StuNum, PhotoPath = ImagePath };
                    OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(user, new string[] { "PhotoPath" });
                    return Content("<script>alert('修改成功')</script>");
                }
                catch (Exception ex)
                {
                    return Content("<script>alert('修改失败')</script>");
                }
            }                 
        }
        #endregion

        #region 获取所有职务数据+public ActionResult BrowsePosition()
        /// <summary>
        /// 获取所有职务数据
        /// </summary>
        /// <returns></returns>
         [Common.Attributes.Skip]
        public ActionResult BrowsePosition()
        {
            var  listPresident = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.President)).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                         Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
                     });
            ViewBag.listPresident = listPresident;

            var PlanLeader = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.PlanLeader)).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                         Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
                     });
            ViewBag.PlanLeader = PlanLeader;

            var StudyLeader = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.StudyLeader)).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                         Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
                     });
            ViewBag.StudyLeader = StudyLeader;

            var StudyMember = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.StudyMember)).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                         Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
                     });
            ViewBag.StudyMember = StudyMember;

            var PlanMmember = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.PlanMmember)).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                         Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
                     });
            ViewBag.PlanMmember = PlanMmember;

            var Financial = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.Financial)).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                         Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
                     });
            ViewBag.Financial = Financial;

            var Minister = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.Minister)).Select(u
                      => new MemberInformationDTO()
                      {
                          StuNum = u.StuNum,
                          StuName = u.StuName,
                          Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
                      });
            ViewBag.Minister = Minister;

            //var TechnicalGuide = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Where(p => p.IsDel == false).Select(r => r.RoleId).Contains(Position.President)).Select(u
            //         => new MemberInformationDTO()
            //         {
            //             StuNum = u.StuNum,
            //             StuName = u.StuName,
            //             Year = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(u.StuNum.Substring(0, 4))).ToString() + "年级",
            //         });
            //ViewBag.TechnicalGuide = TechnicalGuide;

            return View();
        }
        #endregion

        #region 分部门查看成员信息以及录入完成时查看成员信息+ public ActionResult DepartmentInfo()
        /// <summary>
        /// 分部门查看成员信息以及录入完成时查看成员信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentInfo()
        {
            /*判断是否获取页面*/
            string IsPostBack = Request.Form["IsPostBack"];
            if (string.IsNullOrEmpty(IsPostBack))
            {
                List<MODEL.T_Department> department;
                string DepId = Request.QueryString["DepId"];
                if (string.IsNullOrEmpty(DepId))
                {
                    department = OperateContext.Current.BLLSession.IDepartmentBLL.GetListBy(u => u.DepartmentId > 0);
                }
                else
                {
                    int id=Convert.ToInt32(DepId);
                    department = OperateContext.Current.BLLSession.IDepartmentBLL.GetListBy(u => u.DepartmentId ==id );
                }
                ViewBag.department = department;
                return View();
            }
            else
            {
                int DepId = Convert.ToInt32(Request["DepId"]);
                int pageIndex = Convert.ToInt32(Request["pageindex"]);
                Expression<Func<MODEL.T_MemberInformation, bool>> whereLambda;
                if (DepId == 10000||DepId==10010)//DepId=1000或者10010代表获取的是实习生的数据和技术指导的数据
                {

                    whereLambda = u => u.IsDelete == false && (u.T_RoleAct.Select(p => p.RoleId).Contains(10009));//实际实习生的职务编号为10009，这里是数据的表设计的不是很好
                }
                else
                {
                    whereLambda = u => u.IsDelete == false && (u.Department == DepId);
                }
                return PageData(whereLambda, pageIndex);

            }
        }
        #endregion

        #region 获取分页数据，共同调用+ public ActionResult PageData(Expression<Func<MODEL.T_MemberInformation, bool>> whereLambda, int pageIndex)
        public ActionResult PageData(Expression<Func<MODEL.T_MemberInformation, bool>> whereLambda, int pageIndex)
        {
            int totalRecord;

            int pageSize = 10;//页容量固定死为10
            try//为什么异常没有捕捉到
            {
                var list = OperateContext.Current.BLLSession.IMemberInformationBLL.GetPagedList(pageIndex, pageSize,
                   whereLambda, u => u.StuNum, out totalRecord).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                         Major = u.Major,
                         TelephoneNumber = u.TelephoneNumber,
                         Year=(Convert.ToInt32(DateTime.Now.Year)-Convert.ToInt32(u.StuNum.Substring(0,4))).ToString()+"年级",
                         Department =u.T_Department==null?"无":u.T_Department.DepartmentName,
                     });

                JsonModel json;
                PageModel pageModel = new PageModel()
                {
                    TotalRecord = totalRecord,
                    data = list
                };

                json = new JsonModel()
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
            catch (Exception ex)
            {
                return null;
            }


        }
        #endregion
    }
    /*怎么去重写List的tostring方法*/
}