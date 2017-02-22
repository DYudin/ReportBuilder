using Microsoft.Practices.Unity;
using ReportBuilder.Services.UnitOfWork.Abstract;
using System;
using ReportBuilder.Infrastructure.Services.Abstract;
using ReportBuilder.Infrastructure.Services.Implementation;
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

        public static IUnityContainer GetConfiguredContainer()
        {
            return _container.Value;
        }

        public static void RegisterComponents(IUnityContainer container)
        {
            // Context, UoW factory
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(
                new PerRequestLifetimeManager());

            // Services
            container.RegisterType<IReportBuilder, ExcelReportBuilder>(
                new PerRequestLifetimeManager());

            container.RegisterType<IEmailService, CustomEmailService>(
                new PerRequestLifetimeManager(),
                new InjectionConstructor("smtp.yandex.ru", 25));
        }
    }
}