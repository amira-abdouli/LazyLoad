using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LazyLoad.Startup))]
namespace LazyLoad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
