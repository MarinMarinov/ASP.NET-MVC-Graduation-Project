using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Web.ViewModels.Bid
{
    using global::Auction.Models;

    public class BidDetailsViewModel
    {
        public int Value { get; set; }

        public int? CurrentPrice { get; set; }

        public User Winner { get; set; }

        public User Bidder { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<User> Bidders { get; set; }
    }
}