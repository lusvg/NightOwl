using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FurlenCode.Startup))]
namespace FurlenCode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
