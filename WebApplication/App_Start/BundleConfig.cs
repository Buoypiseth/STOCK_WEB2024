using System.Web;
using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/assets/plugins/jquery/jquery.min.js",
                "~/assets/plugins/bootstrap/js/bootstrap.bundle.min.js",
                "~/assets/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                "~/assets/dist/js/adminlte.min.js",
                "~/assets/dist/js/custom.js")
                .IncludeDirectory("~/assets/plugins/jquery", "js")
                .IncludeDirectory("~/assets/plugins/bootstrap/js", "js")
                .IncludeDirectory("~/assets/plugins/overlayScrollbars/js", "js")
                .IncludeDirectory("~/assets/dist/js", "js")
            );

             bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/assets/dist/css/fallback.css",
                "~/assets/plugins/fontawesome-free/css/all.min.css",
                "~/assets/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                "~/assets/dist/css/adminlte.min.css",
                "~/assets/dist/css/custom.css")
                .IncludeDirectory("~/assets/plugins/fontawesome-free/css", "css")
                 .IncludeDirectory("~/assets/plugins/overlayScrollbars/css", "css")
                .IncludeDirectory("~/assets/dist/css", "css")
            );
        }
    }
}
