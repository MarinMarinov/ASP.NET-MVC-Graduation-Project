using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Web.ViewModels.Auction
{
    using global::Auction.Models;

    public class ActiveAuctionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public bool Active { get; set; }

        public int InitialPrice { get; set; }

        public int BidStep { get; set; }

        public string WinnerId { get; set; }

        public string ReceiverId { get; set; }

        public ICollection<Item> Items { get; set; }

        public ICollection<User> Bidders { get; set; }
    }
}