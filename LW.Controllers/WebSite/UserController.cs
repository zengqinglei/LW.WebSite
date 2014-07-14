using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using System.Web.Mvc;
using LW.ViewModels;
#endregion

namespace LW.Controllers.WebSite
{
    public class UserController : Controller
    {
        #region 客户账户--登录
        [HttpGet]
        public ActionResult Login()
        {
            return View(new VM_Base());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName,string password)
        {
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
