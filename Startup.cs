using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gs10.Startup))]
namespace gs10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
