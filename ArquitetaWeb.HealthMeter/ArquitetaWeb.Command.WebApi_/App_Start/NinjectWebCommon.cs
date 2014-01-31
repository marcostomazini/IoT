[assembly: WebActivator.PreApplicationStartMethod(typeof(ArquitetaWeb.Command.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(ArquitetaWeb.Command.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace ArquitetaWeb.Command.WebApi.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using ArquitetaWeb.Command.Entities.Tabelas;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel); // webapi2
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ArquitetaWeb.Common.Infra.Repositorios.IRepository<Device>>().To<ArquitetaWeb.Common.Infra.Repositorios.Repository<Device>>();
            kernel.Bind<ArquitetaWeb.Common.Infra.Repositorios.IRepository<Usuario>>().To<ArquitetaWeb.Common.Infra.Repositorios.Repository<Usuario>>();
            kernel.Bind<ArquitetaWeb.Common.Infra.Repositorios.IRepository<Configuracao>>().To<ArquitetaWeb.Common.Infra.Repositorios.Repository<Configuracao>>();
            kernel.Bind<ArquitetaWeb.Common.Infra.Repositorios.IRepository<Garcom>>().To<ArquitetaWeb.Common.Infra.Repositorios.Repository<Garcom>>();
            kernel.Bind<ArquitetaWeb.Common.Infra.Repositorios.IRepository<Pedido>>().To<ArquitetaWeb.Common.Infra.Repositorios.Repository<Pedido>>();
            kernel.Bind<ArquitetaWeb.Common.Infra.Repositorios.IRepository<Mesa>>().To<ArquitetaWeb.Common.Infra.Repositorios.Repository<Mesa>>();
            kernel.Bind<ArquitetaWeb.Common.Infra.Repositorios.IRepository<Produto>>().To<ArquitetaWeb.Common.Infra.Repositorios.Repository<Produto>>();
        }        
    }
}
