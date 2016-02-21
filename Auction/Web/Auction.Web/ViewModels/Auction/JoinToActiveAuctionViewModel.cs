using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Web.ViewModels.Auction
{
    using System.Linq.Expressions;

    using global::Auction.Models;

    public class JoinToActiveAuctionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfAuction { get; set; }

        public bool Active { get; set; }

        public int InitialPrice { get; set; }

        public int BidStep { get; set; }

        public ICollection<Item> Items { get; set; }
        
    }
}