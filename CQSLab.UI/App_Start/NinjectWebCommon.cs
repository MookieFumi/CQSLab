using System;
using System.Web;
using CQSLab.Business;
using CQSLab.Services;
using CQSLab.UI;
using CQSLab.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]
namespace CQSLab.UI
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));

            kernel.Bind<ModelContext>().ToSelf().InRequestScope();

            kernel.Bind<SecurityContext>().ToSelf().InRequestScope();
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            //kernel.Bind<UserManager<ApplicationUser>>().ToMethod(context => Startup.UserManagerFactory());
            //kernel.Bind(typeof(IUserStore<ApplicationUser>)).To(typeof(UserStore<ApplicationUser>)).InRequestScope();

            kernel.Bind(x => x.FromAssemblyContaining<IProductsService>()
                .SelectAllClasses()
                .BindAllInterfaces());
        }
    }
}
