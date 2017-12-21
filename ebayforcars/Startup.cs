using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ebayforcars.Startup))]
namespace ebayforcars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
