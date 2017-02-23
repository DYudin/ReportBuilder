using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using ReportBuilder;
using ReportBuilder.Infrastructure;

namespace WeatherCatcher
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            UnityWebActivator.Start();

            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperWebConfiguration.Configure();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}