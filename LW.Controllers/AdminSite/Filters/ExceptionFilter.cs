using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
#region 命名空间
using LW.ViewModels;
#endregion

namespace LW.AdminSite.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Controller is Controller)
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
                    {
                        filterContext.Result = new JsonResult()
                        {
                            Data = new Result()
                            {
                                msg = filterContext.Exception.Message
                            },
                            ContentType = "text/html"
                        };
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "error",
                            action = "general"
                        }));
                    }
                }
            }
        }
    }
}