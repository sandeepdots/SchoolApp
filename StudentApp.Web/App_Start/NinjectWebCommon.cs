using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using SchoolApp.Web.App_Start;
using SchoolApp.Repo;
using SchoolApp.Service.AttendanceService;
using SchoolApp.Service.StudentService;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Service.FacultyService;

using SchoolApp.Service.DepartmentServices;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]
namespace SchoolApp.Web.App_Start
{

    public class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            //ValidationConfiguration(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {
            
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<IStudentServices>().To<StudentServices>();
            kernel.Bind<IAttendanceServices>().To<AttendanceServices>();
            kernel.Bind<IDepartmentServices>().To<DepartmentServices>();
            kernel.Bind<IFacultyServices>().To<FacultyServices>();
        }
    }
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.kernel.GetAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }
    }


}