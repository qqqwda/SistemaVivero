using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaViveros.Backend.Startup))]
namespace SistemaViveros.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
