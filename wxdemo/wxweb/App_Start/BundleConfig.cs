using System.Web;
using System.Web.Optimization;

namespace wxweb
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/assets/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/ace").Include(
                      "~/assets/js/ace.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/cm.js"));



            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/mesg.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/assets/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/assets/font-awesome/4.5.0/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/ace").Include("~/assets/css/ace.min.css",
                        "~/assets/css/css-main.css",
                        "~/assets/css/css-addition.css"));


        }
    }
}
