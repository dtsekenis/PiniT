using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PiniT.Startup))]
namespace PiniT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
