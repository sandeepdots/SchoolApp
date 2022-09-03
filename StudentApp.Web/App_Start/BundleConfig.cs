using System.Web;
using System.Web.Optimization;

namespace SchoolApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/core").Include(
                         "~/Content/bs3/css/bootstrap.css",
                         "~/Content/css/bootstrap-reset.css",
                         "~/Content/font-awesome/css/font-awesome.css",
                         "~/Scripts/jvector-map/jquery-jvectormap-1.2.2.css",
                         "~/Content/css/clndr.css",
                         "~/Content/css/bucket-ico-fonts.css"
                 ));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
             "~/Content/css/style.css",
             "~/Content/css/style-responsive.css",
             "~/Content/css/loader.css"
             ));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                "~/Content/bs3/js/bootstrap.js"
                ));
            bundles.Add(new ScriptBundle("~/Scripts/jQuery").Include(
                 "~/Scripts/jquery.js"
               ));
            bundles.Add(new StyleBundle("~/Content/jquery-datatable").Include(
                "~/Scripts/jquery-datatable/css/dataTables.bootstrap.css"
            ));
            bundles.Add(new StyleBundle("~/Content/switch").Include(
               "~/Content/css/bootstrap-switch.css"
               ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/Scripts/switch").Include(
                       "~/Scripts/bootstrap-switch.js"
               ));
            bundles.Add(new ScriptBundle("~/Scripts/jquery-datatable").Include(
                             "~/Scripts/jquery-datatable/js/jquery.dataTables.js",
                             "~/Scripts/jquery-datatable/js/dataTables.bootstrap.js"
              ));
            bundles.Add(new ScriptBundle("~/Scripts/global").Include(
                        "~/Scripts/global.js"
                ));
        }
    }
}
