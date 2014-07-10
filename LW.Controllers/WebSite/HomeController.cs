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
    public class HomeController : Controller
    {
        #region 主页--视图
        [HttpGet]
        public ActionResult Index()
        {
            return View(new VM_Base());
        }
        #endregion
    }
}
