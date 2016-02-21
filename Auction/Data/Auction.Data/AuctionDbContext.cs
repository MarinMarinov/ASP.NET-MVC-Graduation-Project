namespace Auction.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Auction.Models.Common;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class AuctionDbContext : IdentityDbContext<User>, IAuctionDbContext
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

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Bid> Bids { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}