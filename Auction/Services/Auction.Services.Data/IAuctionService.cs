namespace Auction.Services.Data
{
    using System.Linq;
    using System.Web.Mvc;
    using Auction.Models;

    public interface IAuctionService
    {
        IQueryable<Auction> GetAllAuctions();

        Auction GetById(int id);

        IQueryable<SelectListItem> GroupByTypes(ItemType itemType);
    }
}
