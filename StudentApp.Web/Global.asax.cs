using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SchoolApp.Web.Validation;


namespace SchoolApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ValidationConfiguration();
            //Database.SetInitializer<SchoolApp.Models.EmployeeContext>(null);
        }
        //private void ValidationConfiguration()
        //{
        //    FluentValidationModelValidatorProvider.Configure(provider =>
        //    {
        //        provider.ValidatorFactory = new ValidatorFactory();
        //    });
        //}
    }
}
