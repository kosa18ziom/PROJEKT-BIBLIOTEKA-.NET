using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Biblioteka.Startup))]
namespace Biblioteka
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
