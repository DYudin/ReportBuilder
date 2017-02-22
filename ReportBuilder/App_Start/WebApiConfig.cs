
using System.Web.Http;

namespace ReportBuilder
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{Name}",
                defaults: new { Name = RouteParameter.Optional }
            );
        }
    }
}
