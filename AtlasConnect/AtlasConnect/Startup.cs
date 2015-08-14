using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AtlasConnect.Startup))]
namespace AtlasConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
