namespace Auction.Web
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class DbConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuctionDbContext, Configuration>());
            AuctionDbContext.Create().Database.Initialize(true);
        }
    }
}