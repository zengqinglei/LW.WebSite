using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
#region 命名空间
using LW.ViewModels;
using LW.Business;
#endregion

namespace LW.Controllers.AdminSite
{
    public class UserController : Controller
    {
        #region 客户管理--列表
        [HttpGet]
        public ActionResult List()
        {
            return View(new VM_Base());
        }
        [HttpPost]
        public ActionResult List(int page, int rows, string sort = null, string order = null,
            DateTime? regBeginTime = null, DateTime? regEndTime = null, string nickname = null, string usermail = null)
        {
            int total = 0;
            var vmUserList = new B_User().GetListPage(page, rows, out total, sort: sort, order: order,
                regBeginTime: regBeginTime, regEndTime: regEndTime, nickname: nickname, usermail: usermail);

            return Json(new { total = total, rows = vmUserList });
        }
        #endregion

        #region 客户管理--详细
        [HttpGet]
        public ActionResult Detail(int userid)
        {
            return View(new B_User().GetOne(userid));
        }
        #endregion

        #region 客户管理--删除
        [HttpPost]
        public ActionResult Delete(int userid)
        {
            var result = new Result();
            try
            {
                new B_User().Delete(userid);

                result.msg = "删除成功！";
                result.status = 1;
            }
            catch (Exception ex)
            {
                result.msg = ex.Message;
            }
            return Json(result);
        }
        #endregion
    }
}
