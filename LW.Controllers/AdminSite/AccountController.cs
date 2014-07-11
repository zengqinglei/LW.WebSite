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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(VM_MLogin vm_MLogin)
        {
            #region 数据验证
            if (!string.Equals(B_Service.GetValidCode(), vm_MLogin.ValidCode, StringComparison.OrdinalIgnoreCase))
            {
                return Json(new Result("验证码错误！"));
            }
            if (ConfigHelper.GetValue("AdminAccount") != vm_MLogin.Account || ConfigHelper.GetValue("AdminPassword") != EDHelper.MD5Encrypt(vm_MLogin.Password))
            {
                return Json(new Result("账户或密码错误！"));
            }
            #endregion


            FormsAuthentication.SetAuthCookie(vm_MLogin.Account, false);

            var result = new Result();
            if (Request["isAjax"] != "true")
            {
                result.data = Request["backurl"] ?? "/admin";
            }
            result.status = 1;
            result.msg = "登录成功！";

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
    }
}
