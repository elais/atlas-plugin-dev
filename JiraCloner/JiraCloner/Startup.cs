using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JiraCloner.Startup))]
namespace JiraCloner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
