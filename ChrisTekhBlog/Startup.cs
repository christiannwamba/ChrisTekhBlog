using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChrisTekhBlog.Startup))]
namespace ChrisTekhBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
