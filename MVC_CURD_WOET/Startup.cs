using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_CURD_WOET.Startup))]
namespace MVC_CURD_WOET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
