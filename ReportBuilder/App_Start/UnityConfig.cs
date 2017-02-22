using Microsoft.Practices.Unity;
using ReportBuilder.Services.UnitOfWork.Abstract;
using System;
using System.Data.Entity;
using ReportBuilder.Infrastructure.Services.Abstract;
using ReportBuilder.Infrastructure.Services.Implementation;
using ReportBuilder.Infrastructure;
using ReportBuilder.Infrastructure.Repositories.Implementation;
using ReportBuilder.Infrastructure.Repositories.Abstract;
using ReportBuilder.Infrastructure.UnitOfWork.Implementation;

namespace ReportBuilder
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterComponents(container);
            return container;
        });

        private static ReportBuilderContext _context;
        public static IUnityContainer GetConfiguredContainer()
        {
            return _container.Value;
        }

        public static void RegisterComponents(IUnityContainer container)
        {
            // Context, UoW factory
            container.RegisterType<DbContext, ReportBuilderContext>(
                new PerRequestLifetimeManager());
            //var context = container.Resolve<DbContext>();
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(
                new PerRequestLifetimeManager(),
                new InjectionConstructor(container.Resolve<DbContext>()));

            // Repositories
            container.RegisterType<IOrderRepository, OrderRepository>(
                new PerRequestLifetimeManager(),
                new InjectionConstructor(container.Resolve<DbContext>()));
            //var userRepository = container.Resolve<IOrderRepository>();

            // Services
            container.RegisterType<IReportBuilder, ExcelReportBuilder>(
                new PerRequestLifetimeManager());

            container.RegisterType<IEmailService, CustomEmailService>(
                new PerRequestLifetimeManager());
        }
    }
}