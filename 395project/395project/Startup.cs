using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_395project.Startup))]
namespace _395project
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
