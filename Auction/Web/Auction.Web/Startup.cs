using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Auction.Web.Startup))]
namespace Auction.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
