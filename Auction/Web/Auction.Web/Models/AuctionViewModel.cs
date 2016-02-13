namespace Auction.Web.Models
{
    using Auction.Models;
    using System;
    using System.Linq.Expressions;

    public class AuctionViewModel
    {
        public static Expression<Func<Auction, AuctionViewModel>> FromAuction
        {
            get
            {
                return auction => new AuctionViewModel { 
                    Name = auction.Name,
                    DateOfAuction = auction.DateOfAuction,
                    Active = auction.Active,
                    Creator = auction.Creator.UserName
                };
            }
        }

        public string Name { get; set; }

        public DateTime DateOfAuction { get; set; }

        public bool Active { get; set; }

        public string Creator { get; set; }
    }
}