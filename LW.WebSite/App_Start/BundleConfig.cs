using System.Web;
using System.Web.Optimization;

namespace LW.WebSite
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region 绑定脚本
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jqueryval/jquery.unobtrusive*",
                        "~/Scripts/jqueryval/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-easyui").Include(
                        "~/Scripts/jquery-easyui/jquery.easyui.js",
                        "~/Scripts/jquery-easyui/locale/easyui-lang-zh_CN.js",
                        "~/Scripts/jquery-easyui/jquery.easyui-ext.js"));
            // 绑定前台脚本
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            // 绑定后台脚本
            bundles.Add(new ScriptBundle("~/bundles/admin/common").Include(
                        "~/Scripts/Admin/common.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/manager-login").Include(
                        "~/Scripts/Admin/manager.login.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/manager-resetpassword").Include(
                        "~/Scripts/Admin/manager.resetpassword.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/home-index").Include(
                        "~/Scripts/Admin/home.index.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/user-list").Include(
                        "~/Scripts/Admin/user.list.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/user-save").Include(
                        "~/Scripts/Admin/user.save.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/category-list").Include(
                        "~/Scripts/Admin/category.list.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/box-list").Include(
                        "~/Scripts/Admin/box.list.js"));
            #endregion

            #region 绑定样式
            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include("~/Content/bootstrap/bootstrap.css"));

            //绑定前台样式
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            // 绑定后台样式
            bundles.Add(new StyleBundle("~/Content/admin/css").Include("~/Content/Admin/site.css"));
            bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/black/easyui").Include(
                        "~/Content/jquery-easyui/themes/black/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/bootstrap/easyui").Include(
                        "~/Content/jquery-easyui/themes/bootstrap/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/default/easyui").Include(
                        "~/Content/jquery-easyui/themes/default/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/gray/easyui").Include(
                        "~/Content/jquery-easyui/themes/gray/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/metro/easyui").Include(
                        "~/Content/jquery-easyui/themes/metro/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/jquery-easyui/themes/icon").Include(
                        "~/Content/jquery-easyui/themes/icon.css",
                        "~/Content/jquery-easyui/themes/icon-ext.css"));
            bundles.Add(new StyleBundle("~/Content/admin/manager-login").Include("~/Content/admin/manager.login.css"));
            bundles.Add(new StyleBundle("~/Content/admin/manager-resetpassword").Include("~/Content/admin/manager.resetpassword.css"));
            bundles.Add(new StyleBundle("~/Content/admin/home-index").Include("~/Content/admin/home.index.css"));
            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}