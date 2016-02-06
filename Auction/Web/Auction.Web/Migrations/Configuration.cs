namespace Auction.Web.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<AuctionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AuctionDbContext context)
        {
            if(context.Auctions.Any())
            {
                return;
            }

            var seed = new SeedData();
            seed.db.SaveChanges();
        }
    }
}
