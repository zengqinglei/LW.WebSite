using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using System.Web.Mvc;
using LW.ViewModels;
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
        public ActionResult Login(string userName, string password)
        {
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
