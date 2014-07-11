using System.Web;
using System.Web.Mvc;

namespace LW.WebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
namespace LW.AdminSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LW.AdminSite.Filters.ExceptionFilter());
        }
    }
}