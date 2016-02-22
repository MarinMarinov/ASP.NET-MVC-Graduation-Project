namespace Auction.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Auction.Data.Repositories;
    using Auction.Infrastructure.Mapping;
    using Auction.Models;
    using Auction.Services.Data;
    using Auction.Web.ViewModels.Item;

    public class ItemController : Controller
    {
        private IDbRepository<Auction> dataAuction;
        private IDbRepository<Item> dataItem;
        private IDbRepository<User> dataUser;
        private IDbRepository<Image> dataImage;

        public ItemController(IDbRepository<Auction> auctions,
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

        public ActionResult Edit(int id)
        {
            return this.Content("Edit motherfucker");
        }

        [HttpGet]
        public ActionResult CreateItem()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult CreateItem(CreateItemViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var image = new Image
                                    {
                                        FileName = Path.GetFileName(file.FileName),
                                        Extension = Path.GetExtension(file.FileName),
                                        ContentType = file.ContentType,
                                        ContentLength = file.ContentLength,
                                    };

                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        image.ImageArray = reader.ReadBytes(file.ContentLength);
                    }

                    model.Images.Add(image);
                }
            }

            if (this.ModelState.IsValid)
            {
                var item = new Item
                {
                    Title = model.Title,
                    Type = model.Type,
                    Author = model.Author,
                    Description = model.Description,
                    Images = model.Images
                };

                this.dataItem.Add(item);
                this.dataItem.Save();

                return this.RedirectToAction("Index", "Home", new { area = string.Empty});
            }

            return this.View(model);
        }
    }
}