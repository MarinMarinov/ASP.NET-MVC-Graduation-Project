namespace Auction.Web
{
    using System.Data.Entity;
    using Auction.Data;
    using Data.Migrations;


    public class DbConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuctionDbContext, Configuration>());
            AuctionDbContext.Create().Database.Initialize(true);
        }
    }
}