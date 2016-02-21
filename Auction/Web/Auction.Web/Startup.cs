using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Auction.Web.Startup))]
namespace Auction.Web
{
    using Auction.Data;
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Services.Data;
    using Auction.Web.Hubs;
    using Microsoft.AspNet.SignalR;

    public partial class Startup
    {
        private AuctionDbContext db;

        public Startup()
        {
            this.db = new AuctionDbContext();
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalHost.DependencyResolver.Register(
               typeof(AuctionRoom),
               () => new AuctionRoom(new BidsServices(new DbRepository<Bid>(db), new DbRepository<User>(db))));

            app.MapSignalR(); // TODO according to readme.txt
        }
    }
}
