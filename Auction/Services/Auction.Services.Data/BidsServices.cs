﻿namespace Auction.Services.Data
{
    using System.Collections.Generic;

    using Auction.Data.Repositories;
    using Auction.Models;
    using System.Linq;

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

        public Bid Create(int value, int newPrice, string bidderId, string winnerId, int auctionId, IList<string> receiversIds)
        {
            var bid = new Bid
            {
                Value = value,
                NewPrice = newPrice,
                BidderId = bidderId,
                WinnerId = winnerId,
                WinnerUsername = this.users.GetById(winnerId).ToString(),
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