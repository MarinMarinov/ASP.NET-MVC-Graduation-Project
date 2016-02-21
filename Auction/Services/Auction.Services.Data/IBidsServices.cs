namespace Auction.Services.Data
{
    using System.Collections.Generic;
    using Auction.Models;

    public interface IBidsServices
    {
        Bid Create(int value, string bidderId, int auctionId, ICollection<string> receiversIds);
    }
}
