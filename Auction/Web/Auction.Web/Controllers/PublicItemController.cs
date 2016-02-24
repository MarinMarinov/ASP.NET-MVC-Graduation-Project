namespace Auction.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Auction.Data.Repositories;
    using Auction.Infrastructure.Mapping;
    using Auction.Models;
    using Auction.Web.ViewModels.Item;

    public class PublicItemController : BaseController
    {
        private IDbRepository<Item> dataItem;
        private IDbRepository<Image> dataImage;

        public PublicItemController(
            IDbRepository<Item> items,
            IDbRepository<Image> images)
        {
            this.dataItem = items;
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
            var item = this.dataItem.GetById(id);
            var viewModel = this.Mapper.Map<ItemViewModel>(item);

            return this.View(viewModel);
        }

        public FileResult GetImage(int id)
        {
            var image = this.dataImage.GetById(id);

            return new FileContentResult(image.ImageArray, image.ContentType);
        }
    }
}