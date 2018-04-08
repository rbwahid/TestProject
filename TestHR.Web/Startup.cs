using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestHR.Web.Startup))]
namespace TestHR.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
