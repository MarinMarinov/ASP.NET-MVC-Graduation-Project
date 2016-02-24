namespace Auction.Web.ViewModels.Auction
{
    using System;
    using System.Collections.Generic;
    using global::Auction.Infrastructure.Mapping;
    using global::Auction.Models;

    public class AuctionDetailsViewModel : IMapFrom<Auction>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public DateTime DateOfAuction { get; set; }

        public int InitialPrice { get; set; }

        public int BidStep { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
