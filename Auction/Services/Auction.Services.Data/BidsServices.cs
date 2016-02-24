namespace Auction.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using Auction.Data.Repositories;
    using Auction.Models;
    using System.Linq;

    using Auction.Data;

    public class BidsServices : IBidsServices
    {
        private IDbRepository<Bid> bids;
        private IDbRepository<User> users;
        private IDbRepository<Auction> auctions;

        public BidsServices()
        {

        }

        public BidsServices(IDbRepository<Bid> bids, IDbRepository<User> users, IDbRepository<Auction> auctions)
        {
            this.bids = bids;
            this.users = users;
            this.auctions = auctions;
        }

        // TODO: Problems with the Database
        public bool CheckIfAuctionIsActive(int auctionId)
        {
             var auction = this.auctions.GetById(auctionId);
             var isActive = auction.Active;
             return isActive;
        }

        public Bid Create(int value, int currentPrice, string bidderId, string winnerId, int auctionId, IList<string> receiversIds)
        {
            string winnerUsername = string.Empty;

            if (!string.IsNullOrEmpty(winnerId))
            {
                winnerUsername = this.users.GetById(winnerId).UserName.ToString();
            }

            var bid = new Bid
            {
                Value = value,
                CurrentPrice = currentPrice,
                BidderId = bidderId,
                WinnerId = winnerId,
                WinnerUsername = winnerUsername,
                AuctionId = auctionId,
            };

            if (receiversIds[0] == "All")
            {
                bid.Bidders = auctions.GetById(auctionId).Bidders.ToList();
            }
            else
            {
                foreach (var userId in receiversIds)
                {
                    var receiver = this.users.GetById(userId);
                    bid.Bidders.Add(receiver);
                }
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
