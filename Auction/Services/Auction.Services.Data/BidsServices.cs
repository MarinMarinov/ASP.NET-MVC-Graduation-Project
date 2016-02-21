namespace Auction.Services.Data
{
    using System.Collections.Generic;

    using Auction.Data.Repositories;
    using Auction.Models;
    using System.Linq;

    public class BidsServices : IBidsServices
    {
        private IDbRepository<Bid> bids;
        private IDbRepository<User> users;

        public BidsServices(){

        }

        public BidsServices(IDbRepository<Bid> bids, IDbRepository<User> users)
        {
            this.bids = bids;
            this.users = users;
        }

        public Bid Create(int value, string bidderId, int auctionId, ICollection<string> receiversIds)
        {
            var bid = new Bid
            {
                Value = value,
                BidderId = bidderId,
                AuctionId = auctionId
            };

            foreach (var userId in receiversIds)
            {
                var receiver = this.users.GetById(userId);
                bid.Bidders.Add(receiver);
            }

            this.bids.Add(bid);
            this.bids.Save();

            return bid;
        }

        public IQueryable<Bid> LoadUserBidHistory(string userId)
        {
            var allBids = this.bids.All()
                .Where(b => b.BidderId == userId || b.Bidders.Any(u => u.Id == userId));

            return allBids;
        }
    }
}
