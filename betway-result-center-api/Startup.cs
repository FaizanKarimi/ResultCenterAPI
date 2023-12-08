using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(betway_result_center_api.Startup))]
namespace betway_result_center_api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            ConfigureAuth(app);
        }
    }
}