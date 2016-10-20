using System.Web.Optimization;

namespace SnapChallenge
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/snapchallenge/css").Include(
                "~/Content/css/app.min.1.css",
                "~/Content/css/app.min.2.css",
                "~/Content/css/demo.css",
                "~/Content/vendors/ui-grid/ui-grid.min.css"
                ));

            bundles.Add(new ScriptBundle("~/snapchallenge/jquery").Include(
                "~/Content/vendors/bower_components/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/snapchallenge/angular").Include(
                "~/Content/vendors/bower_components/angular/angular.min.js"));

            bundles.Add(new ScriptBundle("~/snapchallenge/angularmodules").Include(
                "~/Content/vendors/bower_components/angular-ui-router/release/angular-ui-router.min.js",
                "~/Content/vendors/bower_components/angular-loading-bar/src/loading-bar.js",
                "~/Content/vendors/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js"));

            bundles.Add(new ScriptBundle("~/snapchallenge/vendorscommon").Include(
                "~/Content/vendors/bootstrap-growl/bootstrap-growl.min.js",
                "~/Content/vendors/ui-grid/ui-grid.min.js"));

            bundles.Add(new ScriptBundle("~/snapchallenge/ie9placeholoder").Include(
                "~/Content/vendors/bower_components/jquery-placeholder/jquery.placeholder.min.js"));

            bundles.Add(new ScriptBundle("~/snapchallenge/app").Include(
                "~/Content/js/app.js",
                "~/Content/js/config.js",
                "~/Content/js/controllers/main.js",
                "~/Content/js/services.js"));
        }
    }
}
