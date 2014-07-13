using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
#region 命名空间
using LW.Controllers.AdminSite.Filters;
using LW.Business;
using LW.ViewModels;
#endregion

namespace LW.Controllers.AdminSite
{
    [FormsAuthorize]
    public class CategoryController : Controller
    {
        #region 产品类别--列表
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult List(string sort = null, string order = null, string cname = null)
        {
            return Json(new { rows = new B_Category().GetChilds(0, hasChild: true) });
        }
        #endregion

        #region 产品类别--详细
        public ActionResult Detail(int cid)
        {
            return View(new B_Category().GetOne(cid));
        }
        #endregion

        #region 产品类别--删除
        public ActionResult Delete(int cid)
        {
            var result = new Result();
            try
            {
                new B_Category().Delete(cid);

                result.status = 1;
                result.msg = "删除成功！";
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
