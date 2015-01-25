using System.Web;
using System.Web.Http;

namespace ExpenseAPI.RESTAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config => config.MapHttpAttributeRoutes());
        }
    }
}
