using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LW.Controllers.AdminSite.Filters
{
    public class FormsAuthorizeAttribute : ActionFilterAttribute
    {
        #region 验证当前用户是否已认证
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller is Controller)
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "account",
                            action = "login",
                            isAjax = true
                        }));
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "account",
                            action = "login",
                            backurl = filterContext.HttpContext.Request.RawUrl,
                        }));
                    }
                }
            }
        }
        #endregion
    }
}