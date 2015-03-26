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
using System.Web;
using System.Web.Mvc;

namespace PersonalManger
{
    public class CheckMemberController : Controller
    {

        #region 返回成员信息查看列表页面+public ActionResult Index()
        /// <summary>
        /// 返回成员信息查看列表页面
        /// </summary>+
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 分页获取数据+public ActionResult GetPageData(FormCollection form)
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult GetPageData(FormCollection form)
        {
            int totalRecord;
            int pageIndex = Convert.ToInt32(form["pageindex"]);
            Expression<Func<MODEL.T_MemberInformation, bool>> whereLambda;
            string dataBy = form["dataBy"];//模糊查寻的条件
            int pageSize = 10;//页容量固定死为10
            //根据dataBy生成不同的查寻条件
            if (!string.IsNullOrEmpty(dataBy))
            {
                whereLambda = u => u.IsDelete == false && (u.StuNum.Contains(dataBy) || u.StuName.Contains(dataBy));
            }
            else
            {
                whereLambda = u => u.IsDelete == false;
            }
            var list = OperateContext.Current.BLLSession.IMemberInformationBLL.GetPagedList(pageIndex, pageSize,
               whereLambda, u => u.StuNum, out totalRecord).Select(u
                 => new MemberInformationDTO()
                 {
                     StuNum = u.StuNum,
                     StuName = u.StuName,
                     Major = u.Major,
                     TelephoneNumber = u.TelephoneNumber,
                     Department = u.T_Department.DepartmentName,//效率比较低
                     TechnicalLevel = u.T_TechnicaLevel.TechLevelName//效率比较低
                 });

            JsonModel json;
            if (!string.IsNullOrEmpty(dataBy))//代表这次是条件搜索查询，list.Count()为总记录数
            {
                totalRecord = list.Count();
            }
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


        public ViewResult PersonalPage()
        {
            /*准备好数据*/
            List<MODEL.T_Department> dep = OperateContext.Current.BLLSession.IDepartmentBLL.GetListBy(u => u.DepartmentId > 0);
            List<MODEL.T_TechnicaLevel> techLeval = OperateContext.Current.BLLSession.ITechnicaLevelBLL.GetListBy(u => u.TechLevelId > 0);
            List<MODEL.T_MemberInformation> members = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.StuNum.Contains("2012"));
            List<MODEL.T_Organization> organization = OperateContext.Current.BLLSession.IOrganizationBLL.GetListBy(u => u.OrganizationId > 0);
            MODEL.T_MemberInformation member = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.StuNum == "201258080133").FirstOrDefault();
            ViewBag.dep = dep;
            ViewBag.techLeval = techLeval;
            ViewBag.members = members;
            ViewBag.member = member;
            ViewBag.organization = organization;
            return View();
        }
        /*管理员修改*/
        public ActionResult AdminEdit(MODEL.T_MemberInformation member)
        {
            if (ModelState.IsValid)
            {
                /*EF修改主键一定要加*/
                string[] proNames = new string[] { "StuNum", "StuName", "Gender", "Email", "LoginPwd", "Class", "Major", "Counselor", "HeadTeacher", "UndergraduateTutor", "TelephoneNumber",
            "HomPhoneNumber","FamilyAddress","Department","TechnicalLevel","StudyGuideNumber","TechnicalGuideNumber","Organization","Sign","OtheInfor"};
                OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(member, proNames);
                return Content("修改成功");
            }
            else
            {
                return Content("修改失败");
            }

        }

        /*上传头像*/
        public ActionResult UpLoadImg(HttpPostedFileBase UpLoadImg)
        {
            string fileName = UpLoadImg.FileName;
            //转换只取得文件名，去掉路径。 
            if (fileName.LastIndexOf("\\") > -1)
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }
            UpLoadImg.SaveAs(Server.MapPath("../../image/img/" + fileName));
            string ImagePath = "../../image/img/" + fileName;
            MODEL.T_MemberInformation user = new MODEL.T_MemberInformation() { PhotoPath = ImagePath };
            OperateContext.Current.BLLSession.IMemberInformationBLL.Modify(user, new string[] { "PhotoPath" });
            return View();
        }

        public ActionResult EntryPosition()
        {
            return View();
        }
        public ActionResult GetEntryData(FormCollection form)
        {
            int pageIndex = Convert.ToInt32(form["pageindex"]);
            int role = Convert.ToInt32(form["role"]);
            string datetime = "";
            if (role == 10009 || role == 10008)
            {
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 1).ToString();
            }
            else
            {
                DateTime dt = DateTime.Now;
                datetime = (dt.Year - 3).ToString();
            }
            int pageSize = 10;//页容量固定死为10
            int totalRecord;
            Expression<Func<MODEL.T_MemberInformation, bool>> whereLambda;
            whereLambda = u => u.IsDelete == false && (u.StuNum.Contains(datetime));
            var list = OperateContext.Current.BLLSession.IMemberInformationBLL.GetPagedList(pageIndex, pageSize,
                whereLambda, u => u.StuNum, out totalRecord).Select(u => new MemberInformationDTO()
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

        public ActionResult EntryPositionEx(FormCollection form)
        {
            string[] StuNums = form["stunums"].Split(new char[] { ';' });
            int role = Convert.ToInt32(form["role"]);
            for (int i = 0; i < StuNums.Length - 1; i++)
            {
                MODEL.T_RoleAct roleAct = new MODEL.T_RoleAct() { RoleId = role, RoleActor = StuNums[i], AddTime = DateTime.Now };
                OperateContext.Current.BLLSession.IRoleActBLL.Add(roleAct);
            }

            return Content("生成成功");
        }
        public ActionResult BrowsePosition() 
        {
            List<MODEL.T_MemberInformation> list1 = new List<MODEL.T_MemberInformation>();
            list1=OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10001)).ToList();
            ViewBag.list1= list1;

            List<MODEL.T_MemberInformation> list2 = new List<MODEL.T_MemberInformation>();
            list2 = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10002)).ToList();
            ViewBag.list2 = list2;

            List<MODEL.T_MemberInformation> list3 = new List<MODEL.T_MemberInformation>();
            list3 = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10003)).ToList();
            ViewBag.list3 = list3;

            List<MODEL.T_MemberInformation> list4 = new List<MODEL.T_MemberInformation>();
            list4 = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10004)).ToList();
            ViewBag.list4 = list4;

            List<MODEL.T_MemberInformation> list5 = new List<MODEL.T_MemberInformation>();
            list5 = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10005)).ToList();
            ViewBag.list5 = list5;

            List<MODEL.T_MemberInformation> list6 = new List<MODEL.T_MemberInformation>();
            list6 = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10006)).ToList();
            ViewBag.list6 = list6;

            List<MODEL.T_MemberInformation> list7 = new List<MODEL.T_MemberInformation>();
            list7 = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10007)).ToList();
            ViewBag.list7 = list7;

            List<MODEL.T_MemberInformation> list8 = new List<MODEL.T_MemberInformation>();
            list8 = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.T_RoleAct.Select(p => p.RoleId).Contains(10008)).ToList();
            ViewBag.list8 = list8;
            return View();
        }


        public ActionResult EntryChoose()
        {
            return View();
        }
    }


    /*怎么去重写List的tostring方法*/
}