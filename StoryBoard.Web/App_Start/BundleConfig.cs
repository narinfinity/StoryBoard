using System.Web;
using System.Web.Optimization;

namespace StoryBoard.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/2014.1.318/kendo.all.min.js",
         // "~/Scripts/kendo/2014.1.318/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/2014.1.318/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));


            //Styles
            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
            //"~/Content/kendo/2015.1.408/kendo.common.min.css", 
            "~/Content/kendo/2014.1.318/kendo.common-bootstrap.min.css",
            "~/Content/kendo/2014.1.318/kendo.default.min.css",
            "~/Content/kendo/2014.1.318/kendo.dataviz.silver.min.css",
            "~/Content/kendo/2014.1.318/kendo.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));


        }
    }
}
