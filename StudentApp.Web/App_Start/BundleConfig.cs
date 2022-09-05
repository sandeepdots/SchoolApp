using System.Web;
using System.Web.Optimization;

namespace SchoolApp
{
    public class BundleConfig
    {
        //For more information on bundling, visit http:/go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Scripts
            #region [Jquery]

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.3.1.min.js"));


            #endregion

            #region Bootstrap Js

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap4").Include(
                  "~/Scripts/bootstrap.bundle.js",
                  "~/Scripts/respond.js"));


            ///bundles for account home page
            bundles.Add(new ScriptBundle("~/bundles/BootstrapJs").Include("~/Scripts/jquery.js",
                "~/Scripts/bootstrap.js"));

            #endregion

            #region DataTables
            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
               "~/Scripts/jquery.dataTables.js",
               "~/Scripts/dataTables.bootstrap4.js"
               ));
            #endregion

            #region Admin Js



            bundles.Add(new ScriptBundle("~/bundles/AdminJs").Include(
                    "~/Scripts/adminlte.js",
                     "~/Scripts/jquery.dataTables.js",
                    "~/Scripts/dataTables.bootstrap.js",
                    "~/Scripts/jquery.fnSetFilteringEnterPress.js",
                    "~/Scripts/icheck.min.js",
                      "~/Scripts/jquery-scrolltofixed.js",
                         "~/Scripts/jquery.matchHeight.js",
                    "~/Scripts/custom.js"
                   ));

            #endregion

            #region [Jquery Masked]
            bundles.Add(new ScriptBundle("~/bundles/maskedjs").Include("~/Scripts/jquery.mask.min.js"));
            #endregion
            #region Layout Js

            bundles.Add(new ScriptBundle("~/bundles/layoutjs").Include(
        "~/Scripts/jquery-scrolltofixed.js",
        "~/Scripts/owl.carousel-min.js",
        "~/Scripts/jquery.matchHeight.js",
        "~/Scripts/custom.js",
        "~/Scripts/jquery.dataTables.js",
        "~/Scripts/dataTables.bootstrap.js",
        "~/Scripts/jquery.fnSetFilteringEnterPress.js"));
            #endregion

            #region User Specific


            #region [ Form Bundles ]

        
         
        

            bundles.Add(new StyleBundle("~/bundles/select2-JS").Include("~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/SupportJs").Include(
                      "~/Areas/Support/Scripts/adminlte.js",
                       "~/Areas/Support/Scripts/jquery.dataTables.js",
                      "~/Areas/Support/Scripts/dataTables.bootstrap.js",
                      "~/Scripts/jquery.fnSetFilteringEnterPress.js",
                      "~/Areas/Support/Scripts/icheck.min.js",
                        "~/Scripts/jquery-scrolltofixed.js",
                           "~/Scripts/jquery.matchHeight.js",
                      "~/Scripts/custom.js"
                     ));


          
            #endregion [ Form Bundles ]

            #region [ Users Area Script ]

            bundles.Add(new ScriptBundle("~/bundles/appJS").Include(
                "~/Scripts/alertify/alertify.js",
                "~/Scripts/bootstrap-switch.js", "~/Scripts/InputFilter.js", "~/Scripts/jquery.popover_custom.js"
                ));



            bundles.Add(new ScriptBundle("~/bundles/globalJs").Include("~/Scripts/global.js"));
        
            //common method is used to handle session . please don't remove it from this bundle
            bundles.Add(new ScriptBundle("~/bundles/validateJs").Include(
              "~/Scripts/jquery.validate.min.js",
              "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new StyleBundle("~/bundles/datewithmomentJs").Include(
                        "~/Scripts/datepicker/moment-with-locales.js",
                        "~/Scripts/datepicker/bootstrap-datepicker.js"
                ));
            bundles.Add(new StyleBundle("~/bundles/datetimewithmomentJs").Include(
                        "~/Scripts/datepicker/moment-with-locales.js",
                          "~/Scripts/bootstrap-datetimepicker.js"
                ));
            bundles.Add(new StyleBundle("~/bundles/formChanges").Include("~/Areas/Users/Scripts/formchanges.js"));



            #endregion [ Users Area Script ]


            #endregion

            #region [Modernizer]

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http:/modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            #endregion

            #region [Knock Out]

           
            #endregion

            #region [Datatbale Extension]


            bundles.Add(new ScriptBundle("~/bundles/DataTableExtension").Include("~/Scripts/datatable-extension/dataTables.buttons.min.js",
          "~/Scripts/datatable-extension/buttons.html5.min.js",
          "~/Scripts/datatable-extension/buttons.print.min.js",
          "~/Scripts/datatable-extension/export_excel.min.js"));

            #endregion

            #region FullCalenderJs

           
            #endregion


            #endregion

            #region [CSS]

            #region Admin CSS

            bundles.Add(new StyleBundle("~/bundles/AdminCss").Include(
               "~/Content/css/bootstrap.css",
               "~/Content/css/font-awesome.css",
               "~/Content/css/ionicons.css",
               "~/Content/css/AdminLTE.css",
               "~/Content/css/_all-skins.css",
               "~/Content/css/blue.css",
               "~/Content/css/loader.css",
                "~/Content/css/admin-custom-style.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/alertify/alertify.min.css", "~/Content/css/popover_custom.css"
              ));

            bundles.Add(new StyleBundle("~/bundles/FrontCss").Include(
              "~/Content/css/bootstrap.css",
              "~/Content/css/font-awesome.css",
              "~/Content/css/ionicons.css",
              "~/Content/css/AdminLTE.css",
              "~/Content/css/_all-skins.css",
              "~/Content/css/blue.css",
              "~/Content/css/loader.css",
               "~/Content/css/custom-style.css",
               "~/Content/bootstrap-datepicker.css",
               "~/Content/alertify/alertify.min.css", "~/Content/css/popover_custom.css"
             ));



            #endregion

            #region [Layout CSS]

            bundles.Add(new StyleBundle("~/bundles/layoutcss").Include(
               "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.css",
                 "~/Content/ionicons.css",
               "~/Content/all.css",
               "~/Content/style.css",
               "~/Content/responsive.css",
                 "~/Content/css/loader.css"

                ));

            #endregion

            #region [DATA TABLE]

            bundles.Add(new StyleBundle("~/bundles/DataTablesCss").Include(
             "~/Content/css/dataTables.bootstrap4.min.css"
             ));

            #endregion

            #region [COMMON]

        

            bundles.Add(new ScriptBundle("~/bundles/BundleBootstrapJs").Include("~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/bundles/curtomCss").Include(
                  "~/Content/custom-style.css"
                ));
          

            bundles.Add(new StyleBundle("~/bundles/appcss").Include(
                      "~/Content/css/bootstrap-switch.css",
                      "~/Content/alertify/alertify.min.css"
                      ));


            #endregion

            #region [ADMIN LTE]


            bundles.Add(new StyleBundle("~/bundles/AdminLTE").Include(
                      "~/Content/css/AdminLTE.css",
                      "~/Content/css/_all-skins.css",
                      "~/Content/css/blue.css",
                      "~/Content/css/loader.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-admin-css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/ionicons.css"));

            #endregion

            #region [  Users Area CSS  ]

            bundles.Add(new StyleBundle("~/bundles/customUserStyle").Include(
                        "~/Content/css/custom-style.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/usersstyle-css").Include(
                      "~/Areas/Users/Content/css/admin.css",
                      "~/Areas/Users/Content/css/admin-responsive.css",
                      "~/Content/alertify/alertify.min.css"
                      ));




            bundles.Add(new StyleBundle("~/bundles/bootstrapDatepickerCss").Include("~/Content/bootstrap-datepicker.css"));
            bundles.Add(new StyleBundle("~/bundles/bootstrapDateTimepickerCss").Include("~/Content/css/datepicker/bootstrap-datetimepicker.min.css"));
            bundles.Add(new StyleBundle("~/bundles/bootstrapStepTabCss").Include("~/Areas/Users/Content/css/bootstrap-steps.css"));
            bundles.Add(new StyleBundle("~/bundles/select2-css").Include("~/Content/css/select2.css"));

            #endregion [  Users Area CSS  ]


            #endregion


            #region [IntTelinput]
            bundles.Add(new ScriptBundle("~/bundles/IntTeInputCss").Include("~/Content/intlTelInput.css"));
            bundles.Add(new ScriptBundle("~/bundles/IntTeInputJs").Include("~/Content/intlTelInput.min.js"));



            #endregion


            BundleTable.EnableOptimizations = false;
        }
    }
}
