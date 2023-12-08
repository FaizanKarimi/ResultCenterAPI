using betway_result_center_api.Filters;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace betway_result_center_api.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.EnableCors();
            config.MapHttpAttributeRoutes();

            #region Filters
            config.Filters.Add(new CustomExceptionFilter());
            #endregion

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
