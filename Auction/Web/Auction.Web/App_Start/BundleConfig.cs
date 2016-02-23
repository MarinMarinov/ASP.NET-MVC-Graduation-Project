using System.Web.Optimization;

namespace Auction.Web
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

            bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive-ajax")
                .Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui")
                .Include("~/Scripts/jquery-ui-1.11.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Spacelab.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/jquery-ui")
                .Include("~/Content/jquery-ui.structure.css",
                        "~/Content/jquery-ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                     "~/Scripts/gridmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/gridmvc").Include(
                        "~/Content/Gridmvc.css"));

            // Set EnableOptimizations to false for debugging
            BundleTable.EnableOptimizations = false;
        }
    }
}
