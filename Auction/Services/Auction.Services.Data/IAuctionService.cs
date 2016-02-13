namespace Auction.Services.Data
{
    using System.Linq;
    using Auction.Data.Repositories;
    using Auction.Models;

    public interface IAuctionService
    {
        IQueryable<Auction> GetAllAuctions();
    }


}
