using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecuredSystem.Startup))]
namespace SecuredSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
