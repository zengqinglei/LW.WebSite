using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using System.Web.Mvc;
using LW.ViewModels;
using LW.ViewModels.WebSite;
using LW.Business;
using System.Web.Security;
#endregion

namespace LW.Controllers.WebSite
{
    public class UserController : Controller
    {
        #region 客户账户--注册
        [HttpGet]
        public ActionResult Register()
        {
            return View(new VM_Register());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(VM_Register vmRegister)
        {
            if (!vmRegister.ValidCode.Equals(B_Service.GetValidCode(), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("ValidCode", "验证码错误！");
            }
            if (ModelState.IsValid)
            {
                new B_User().Register(vmRegister);

                return RedirectToAction("Login", "User");
            }
            return View(vmRegister);
        }
        #endregion

        #region 客户账户--登录
        [HttpGet]
        public ActionResult Login()
        {
            return View(new VM_Login());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(VM_Login vmLogin)
        {
            if (ModelState.IsValid)
            {
                if (new B_User().Login(vmLogin))
                {
                    FormsAuthentication.SetAuthCookie(vmLogin.UserMail, false);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("submit-error", "账号或密码错误！");
            }
            return View(vmLogin);
        }
        #endregion
    }
}
