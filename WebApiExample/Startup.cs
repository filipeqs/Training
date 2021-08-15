using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApiExample.Startup))]
namespace WebApiExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
