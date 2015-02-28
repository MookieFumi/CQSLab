using System;
using System.Web;
using CQSLab.Entities;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using CQSLab.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CQSLab.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CQSLab.UI.App_Start.NinjectWebCommon), "Stop")]
namespace CQSLab.UI.App_Start
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
            kernel.Bind<IProductsService>().To<ProductsService>().InRequestScope();
            kernel.Bind<ICustomersService>().To<CustomersService>().InRequestScope();
            kernel.Bind<IOrdersService>().To<OrdersService>().InRequestScope();
            kernel.Bind<IChannelsService>().To<ChannelsService>().InRequestScope();
            kernel.Bind<IStoresService>().To<StoresService>().InRequestScope();
        }        
    }
}
