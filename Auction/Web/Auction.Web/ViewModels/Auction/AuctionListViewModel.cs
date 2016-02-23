namespace Auction.Web.ViewModels.Auction
{
    using System.Collections.Generic;

    public class AuctionListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<ActiveAuctionViewModel> Auctions { get; set; }
    }
}
