namespace Auction.Web.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class AuctionDbContext : IdentityDbContext<User>
    {
        public AuctionDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static AuctionDbContext Create()
        {
            return new AuctionDbContext();
        }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<Auction> Auctions { get; set; }
    }
}