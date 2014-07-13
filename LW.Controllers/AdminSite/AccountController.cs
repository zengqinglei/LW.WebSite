using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using System.Web.Mvc;
using LW.ViewModels;
using LW.Utility;
using LW.Business;
using System.Web.Security;
using LW.Controllers.AdminSite.Filters;
#endregion

namespace LW.Controllers.AdminSite
{
    public class AccountController : Controller
    {
        #region 账户管理--登录
        [HttpGet]
        public ActionResult Login()
        {
            return View(new VM_Base());
        }
        [HttpGet]
        public ActionResult _Login()
        {
            return View(new VM_Base());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account, string password, string validcode)
        {
            var result = new Result();
            try
            {
                #region 数据验证
                if (!string.Equals(B_Service.GetValidCode(), validcode, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("验证码错误！");
                }
                if (ConfigHelper.GetValue("AdminAccount") != account || ConfigHelper.GetValue("AdminPassword") != EDHelper.MD5Encrypt(password))
                {
                    throw new Exception("账户或密码错误！");
                }
                #endregion

                FormsAuthentication.SetAuthCookie(account, false);

                result.data = Request["backurl"] ?? "/admin";
                result.status = 1;
                result.msg = "登录成功！";
            }
            catch (Exception ex)
            {
                result.msg = ex.Message;
            }
            return Json(result, "text/html");
        }
        #endregion

        #region 账户管理--登出
        [HttpGet]
        [FormsAuthorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region 账户管理--重置密码
        [HttpPost]
        [FormsAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string oldpassword, string newpassword)
        {
            var result = new Result();
            try
            {
                if (ConfigHelper.GetValue("AdminPassword") != EDHelper.MD5Encrypt(oldpassword))
                {
                    throw new Exception("原密码不正确！");
                }
                ConfigHelper.UpdateConfigValueOfKey("AdminPassword", EDHelper.MD5Encrypt(newpassword));

                result.msg = "密码修改成功！";
                result.status = 1;
            }
            catch (Exception ex)
            {
                result.msg = ex.Message;
            }
            return Json(result, "text/html");
        }
        #endregion
    }
}
