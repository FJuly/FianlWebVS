using MODEL.DTO;
using MVC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Duty.Controllers
{
    public class DutyController : Controller
    {
        public ActionResult EntryDutyView()
        {
            DateTime dt = DateTime.Now;
            string datetime = (dt.Year - 1).ToString();
            var list = OperateContext.Current.BLLSession.IMemberInformationBLL.GetListBy(u => u.IsDelete == false && (u.StuNum.Contains(datetime))).Select(u
                     => new MemberInformationDTO()
                     {
                         StuNum = u.StuNum,
                         StuName = u.StuName,
                     });
            /*获取大一列表*/
            ViewBag.Stu = list;
            return View();
        }

        [Common.Attributes.AjaxRequest]
        public ActionResult EntryDuty()
        {
            string str = Request.Form["Str"];
            string[] strs = str.Split(new char[] { ';' });
            for (int i = 0; i < strs.Length - 1; i++)
            {
                string[] stu = strs[i].Split(new char[] { ',' });
                MODEL.T_OnDuty duty = new MODEL.T_OnDuty()
                {
                    StuNum = stu[1],
                    Week = Convert.ToInt32(stu[0])
                };
                try
                {
                    OperateContext.Current.BLLSession.IOnDutyBLL.Add(duty);
                }
                catch (Exception ex)
                {
                    return OperateContext.Current.RedirectAjax("err", "修改失败", null, null);
                }
            }
            return OperateContext.Current.RedirectAjax("ok", "修改成功", null, null);

        }
        public ActionResult Index()
        {
            List<int> listWeek = OperateContext.Current.BLLSession.IOnDutyBLL.GetListBy(u => u.IsDelete == false).Select(p => p.Week).ToList();
            /*查询出当前一共有多少周*/
            List<int> listWeeks = listWeek.Distinct().ToList();
            /*这种排序跟linq有什么区别*/
            listWeeks.Sort();
            List<List<MODEL.T_OnDuty>> listdutyCollec = new List<List<MODEL.T_OnDuty>>(); ;//以周为单位值日名单放到listdutyCollec中
            foreach (int i in listWeeks)
            {
                listdutyCollec.Add(OperateContext.Current.BLLSession.IOnDutyBLL.GetListBy(u => u.IsDelete == false).Where(p => p.Week == i).ToList());
            }
            ViewBag.listdutyCollec = listdutyCollec;

            bool IsEdit = false;//判断有没有编辑的权限，前天是否显示编辑按钮
            if (OperateContext.Current.HasPemission("Duty", "Duty", "Delete","post"))
            {
                IsEdit = true;
            }
            ViewBag.IsEdit = IsEdit;
            return View();
        }

        [Common.Attributes.AjaxRequest]
        public ActionResult Delete()
        {
            String Num = Request.Form["Num"];
            try
            {
                if (OperateContext.Current.BLLSession.IOnDutyBLL.DelBy(u => u.StuNum == Num) > 0)
                {
                    return OperateContext.Current.RedirectAjax("ok", "删除成功", null, "/Duty/Duty/Index");
                }
                else
                {
                    return OperateContext.Current.RedirectAjax("err", "删除失败", null, null);
                }
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", ex.Message, null, null);
            }
        }
    }
}
