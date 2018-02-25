using System.Web;
using System.Web.Optimization;

namespace Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                "~/Scripts/toastmessage/js/jquery.toastmessage.js",
                "~/Scripts/linq-vsdoc.js",
                "~/Scripts/linq.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/jquery.mask.js",
                "~/Content/js/app.js",
                "~/Content/js/controllers.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Scripts/toastmessage/rsc/css/jquery.toastmessage.css",
                      "~/Content/site.css"));
        }
    }
}
