namespace Auction.Services.Data
{
    using System.Collections.Generic;
    using Auction.Models;

    public interface IBidsServices
    {
        Bid Create(int value, int newPrice, string bidderId, string winnerId, int auctionId, IList<string> receiversIds);
    }
}
