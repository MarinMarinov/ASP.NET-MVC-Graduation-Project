using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    using System.IO;

    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Services.Data;
    using Auction.Web.ViewModels.Item;

    public class ItemController : Controller
    {
        private IDbRepository<Auction, int> dataAuction;
        private IDbRepository<Item, int> dataItem;
        private IDbRepository<User, string> dataUser;
        private IDbRepository<Image, int> dataImage;

        public ItemController(IDbRepository<Auction, int> auctions,
            IDbRepository<Item, int> items,
            IDbRepository<User, string> users,
            IAuctionService service,
            IDbRepository<Image, int> images)
        {
            this.dataAuction = auctions;
            this.dataItem = items;
            this.dataUser = users;
            this.dataImage = images;
        }

        [HttpGet]
        public ActionResult CreateItem()
        {
            return View();
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

            if (ModelState.IsValid)
            {
                var item = new Item {
                    Title = model.Title,
                    Type = model.Type,
                    Author = model.Author,
                    Description = model.Description,
                    Images = model.Images
                };

                this.dataItem.Add(item);
                this.dataItem.Save();

                return this.RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}