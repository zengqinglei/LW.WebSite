using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
#region 命名空间
using LW.ViewModels;
using LW.Business;
using LW.ViewModels.AdminSite;
using LW.Controllers.AdminSite.Filters;
#endregion

namespace LW.Controllers.AdminSite
{
    [FormsAuthorize]
    public class BoxController : Controller
    {
        #region 产品盒子--列表
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult List(int page, int rows, string sort = null, string order = null,
            DateTime? addBeginTime = null, DateTime? addEndTime = null, string name = null)
        {
            int total = 0;
            var vmBoxList = new B_Box().GetListPage(page, rows, out total, sort: sort, order: order,
                addBeginTime: addBeginTime, addEndTime: addEndTime,name:name);

            return Json(new { total = total, rows = vmBoxList });
        }
        #endregion

        #region 产品盒子--详细
        [HttpGet]
        public ActionResult Detail(int boxid)
        {
            return View(new B_Box().GetOne(boxid));
        }
        #endregion

        #region 产品盒子--保存
        [HttpGet]
        public ActionResult Save(int? boxid)
        {
            VM_Box vmBox = null;
            if (boxid.HasValue)
            {
                vmBox = new B_Box().GetOne(boxid.Value);
            }
            else
            {
                vmBox = new VM_Box();
            }
            return View(vmBox);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(VM_Box vmBox)
        {
            var result = new Result();
            try
            {
                if (vmBox.boxid.HasValue)
                {
                    result.data = new B_Box().Update(vmBox);
                    result.msg = "修改产品成功！";
                }
                else
                {
                    result.data = new B_Box().Add(vmBox);
                    result.msg = "新增产品成功";
                }
                result.status = 1;
            }
            catch (Exception ex)
            {
                result.msg = ex.Message;
            }
            return Json(result, "text/html");
        }
        #endregion

        #region 产品盒子--删除
        [HttpPost]
        public ActionResult Delete(int boxid)
        {
            var result = new Result();
            try
            {
                new B_Box().Delete(boxid);

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
