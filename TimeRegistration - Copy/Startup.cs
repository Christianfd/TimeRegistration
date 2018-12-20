using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeRegistration.Startup))]
namespace TimeRegistration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
