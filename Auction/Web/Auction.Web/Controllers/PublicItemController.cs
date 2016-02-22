namespace Auction.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Auction.Data.Repositories;
    using Auction.Infrastructure.Mapping;
    using Auction.Models;
    using Auction.Services.Data;
    using Auction.Web.ViewModels.Item;

    public class PublicItemController : BaseController
    {
        private IDbRepository<Auction> dataAuction;
        private IDbRepository<Item> dataItem;
        private IDbRepository<User> dataUser;
        private IDbRepository<Image> dataImage;

        public PublicItemController(IDbRepository<Auction> auctions,
            IDbRepository<Item> items,
            IDbRepository<User> users,
            IAuctionService service,
            IDbRepository<Image> images)
        {
            this.dataAuction = auctions;
            this.dataItem = items;
            this.dataUser = users;
            this.dataImage = images;
        }

        [HttpGet]
        public ActionResult ListAllItems()
        {
            IQueryable<ItemViewModel> items = this.dataItem.All().OrderBy(x => x.Id).To<ItemViewModel>();
            return this.View(items);
        }

        [HttpGet]
        public ActionResult ItemDetails(int id)
        {
            return Content("Details");
        }
    }
}