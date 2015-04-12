using Common.Attributes;
using MODEL.FormatModel;
using MVC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        #region 获取登陆页面+public ActionResult Index()
        //
        // GET: /Login/
        /// <summary>
        /// 获取登陆页面，不需要验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common.Attributes.Skip]
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                MODEL.ViewModel.LoginUser user = Session["User"] as MODEL.ViewModel.LoginUser;
                ViewData["Name"] = user.LoginName;
                ViewData["Pwd"] = user.Pwd;
            }
            return View();
        }
        #endregion

        #region 登陆，表单提交过来的数据+public ActionResult Login(MODEL.ViewModel.LoginUser user)
        [Common.Attributes.Skip]
        /// <summary>
        /// 登陆，表单提交过来的数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult Login(MODEL.ViewModel.LoginUser user)
        {
            if (ModelState.IsValid)
            {
                if (OperateContext.Current.UserLogin(user))
                {
                    string VCode=Request.Form["VCode"];
                    string VCodeSer = (string)Session["VCode"];
                    if (VCode.Equals(VCodeSer))
                    {
                        //登陆成功进入主页
                        return new RedirectResult("/Login/Login/MainPage");
                    }
                    else
                    {
                        return OperateContext.Current.RedirectAjax("err", "验证码错误", null, null);
                    }
                }
                else
                {
                    return OperateContext.Current.RedirectAjax("err", "登陆失败", null, null);
                }
            }
            else
            {
                return OperateContext.Current.RedirectAjax("err", "登陆失败", null, null);
            }
        } 
        #endregion

        #region 登陆成功进入主页+ public ActionResult MainPage()
        /// <summary>
        /// 登陆成功进入主页
        /// </summary>
        /// <returns></returns>
        public ActionResult MainPage()
        {
            return View();
        } 
        #endregion

        #region  获取验证码，不需要验证+public ActionResult GetVCode()
        /// <summary>
        /// 获取验证码，不需要验证
        /// </summary>
        /// <returns></returns>
        [Common.Attributes.Skip]
        public ActionResult GetVCode()
        {
            VCode v = new VCode();
            byte[] arrImg = v.GetVCode();
            return File(arrImg, "image/jpeg");
        } 
        #endregion

        #region 3.0 根据当前登陆用户 权限 生成菜单 +GetMenuData()
        /// <summary>
        /// 根据当前登陆用户 权限 生成菜单
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult GetMenuData()
        {
            return Content(OperateContext.Current.UsrMenuJsonStr);
        }
        #endregion

    }
}
