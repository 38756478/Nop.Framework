using System.Linq;
using Autofac;
using Autofac.Integration.Mvc;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Services.Tests;

namespace Nop.Web
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //ע��ObjectContext
            builder.Register<IDbContext>(c => new NopObjectContext("test")).InstancePerLifetimeScope();

            // ע��ef���ִ�
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // ע��Service���ӿ�
            builder.RegisterAssemblyTypes(typeof(TestService).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
             
            //ע��controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }
    }
}
