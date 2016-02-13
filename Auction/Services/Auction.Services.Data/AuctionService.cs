namespace Auction.Services.Data
{
    using System.Linq;

    using Auction.Data.Repositories;
    using Auction.Models;

    public class AuctionService : IAuctionService
    {
        private IDbRepository<Auction, int> auctionRepo;

        /*public AuctionService()
        {
        }*/

        public AuctionService(IDbRepository<Auction, int> auctionRepo)
        {
            this.auctionRepo = auctionRepo;
        }

        public IQueryable<Auction> GetAllAuctions()
        {
            return this.auctionRepo.All().OrderBy(a => a.DateOfAuction);
        }
    }
}
