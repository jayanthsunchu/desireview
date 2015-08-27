using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(desireview.Startup))]
namespace desireview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
