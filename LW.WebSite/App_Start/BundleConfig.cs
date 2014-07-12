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
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-easyui").Include(
                        "~/Scripts/jquery-easyui/jquery.easyui.js",
                        "~/Scripts/jquery-easyui/locale/easyui-lang-zh_CN.js",
                        "~/Scripts/jquery-easyui/jquery.easyui-ext.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/common").Include(
                        "~/Scripts/Admin/common.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/account-login").Include(
                        "~/Scripts/Admin/account.login.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/home-index").Include(
                        "~/Scripts/Admin/home.index.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/user-list").Include(
                        "~/Scripts/Admin/user.list.js"));
            #endregion

            #region 绑定样式
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admin/css").Include("~/Content/Admin/site.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
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
            bundles.Add(new StyleBundle("~/Content/admin/account-login").Include("~/Content/admin/account.login.css"));
            bundles.Add(new StyleBundle("~/Content/admin/home-index").Include("~/Content/admin/home.index.css"));
            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}