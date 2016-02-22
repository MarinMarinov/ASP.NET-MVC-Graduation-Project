namespace Auction.Services.Data
{
    using System.Collections.Generic;
    using Auction.Models;

    public interface IBidsServices
    {
        bool CheckIfAuctionIsActive(int auctionId);

        Bid Create(int value, int newPrice, string bidderId, string winnerId, int auctionId, IList<string> receiversIds);
    }
}
