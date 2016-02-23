namespace Auction.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Services.Data;
    using Auction.Web.Controllers;
    using Auction.Web.ViewModels.Item;

    public class ItemController : BaseController
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
        public ActionResult Edit(int id)
        {
            var item = this.dataItem.GetById(id);
            var modelView = this.Mapper.Map<ItemViewModel>(item);

            return this.View(modelView);
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

        public FileResult GetImage(int id)
        {
            var image = this.dataImage.GetById(id);

            return new FileContentResult(image.ImageArray, image.ContentType);
        }

        [HttpPost]
        public ActionResult Edit(ItemViewModel model, IEnumerable<HttpPostedFileBase> files)
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
                var item = this.dataItem.GetById(model.Id);

                item.Title = model.Title;
                item.Type = model.Type;
                item.Author = model.Author;
                item.Description = model.Description;
                item.Images = model.Images;

                this.dataItem.Save();

                this.TempData["Updated"] = "You have successfully updated the item!";

                return this.RedirectToAction("ListAllItems", "PublicItem", new { area = string.Empty });
            }

            return this.View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = this.dataItem.GetById(id); // TODO Fix the Delete with Images!!!
            this.dataItem.HardDelete(item);
            this.dataItem.Save();

            this.TempData["Deleted"] = "You have successfully deleted the item!";

            return this.RedirectToAction("ListAllItems", "PublicItem", new { area = string.Empty });
        }
    }
}