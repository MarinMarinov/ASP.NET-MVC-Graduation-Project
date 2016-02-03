namespace Auction.Web.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using System.Data.Entity;

    public class AuctionDbContext : IdentityDbContext<User>
    {
        public AuctionDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuctionDbContext, Configuration>());
        }

        public static AuctionDbContext Create()
        {
            return new AuctionDbContext();
        }
    }
}