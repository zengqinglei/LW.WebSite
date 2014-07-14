using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
#region 命名空间
using System.Net;
#endregion

namespace LW.Controllers.AdminSite.Filters
{
    public class FormsAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.Controller is Controller)
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
                    {
                        filterContext.HttpContext.SkipAuthorization = true;
                        filterContext.HttpContext.Response.Clear();
                        filterContext.Result = new JsonResult()
                        {
                            Data = new
                            {
                                url = new UrlHelper(filterContext.RequestContext).Action("_login", "manager"),
                                backurl = filterContext.HttpContext.Request.RawUrl
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                        filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        filterContext.HttpContext.Response.End();
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "manager",
                            action = "login",
                            backurl = filterContext.HttpContext.Request.RawUrl
                        }));
                    }
                }
            }
        }
    }
}