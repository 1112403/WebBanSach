using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WedSiteBanSach.Startup))]
namespace WedSiteBanSach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
