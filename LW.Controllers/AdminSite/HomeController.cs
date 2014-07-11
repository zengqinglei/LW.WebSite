using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using System.Web.Mvc;
using LW.ViewModels;
using LW.Controllers.AdminSite.Filters;
#endregion

namespace LW.Controllers.AdminSite
{
    [FormsAuthorize]
    public class HomeController : Controller
    {
        #region 主页--视图
        [HttpGet]
        public ActionResult Index()
        {
            return View(new VM_Base());
        }
        #endregion

        #region 主页--欢迎
        [HttpGet]
        public ActionResult Welcome()
        {
            return PartialView("Welcome");
        }
        #endregion

        #region 菜单--获取导航
        [HttpPost]
        public ActionResult GetNavs()
        {
            return File(Server.MapPath("~/App_Data/navs.json"), "application/octet-stream", "导航菜单.json");
        }
        #endregion
    }
}
