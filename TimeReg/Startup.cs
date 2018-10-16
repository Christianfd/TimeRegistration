using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeReg.Startup))]
namespace TimeReg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
