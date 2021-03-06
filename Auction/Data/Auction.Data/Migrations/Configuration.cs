namespace Auction.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;

    public sealed class Configuration : DbMigrationsConfiguration<AuctionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AuctionDbContext context)
        {
            var seeder = new SeedData();

            if (!context.Roles.Any())
            {
                seeder.SeedAdminRole(context);
            }

            if (!context.Users.Any())
            {
                seeder.SeedUsers(context);
            }

            if (!context.Items.Any())
            {
                seeder.SeedItems(context);
            }

            if (!context.Auctions.Any())
            {
                seeder.SeedAuctions(context);
            }
        }
    }
}
