
using System.Web.Http;
using ReportBuilder.Filters;

namespace ReportBuilder
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new CustomExceptionAttribute());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{Name}",
                defaults: new { Name = RouteParameter.Optional }
            );
        }
    }
}
