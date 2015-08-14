using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoWatch.Startup))]
namespace AutoWatch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
