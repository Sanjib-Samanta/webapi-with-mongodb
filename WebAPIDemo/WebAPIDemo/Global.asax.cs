using WebAPIDemo.App_Start;
using System.Web.Http;
using System.Web.Mvc;

namespace WebAPIDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Initializes and configues the application during application start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            AutofacWebApiConfig.Initialize(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
           
        }
    }
}
