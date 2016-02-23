namespace Auction.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Services.Data;
    using Auction.Web.Controllers;
    using Auction.Web.ViewModels.Auction;

    public class AuctionController : BaseController
    {
        private IDbRepository<Auction> dataAuction;
        private IDbRepository<Item> dataItem;
        private IDbRepository<User> dataUser;
        private IAuctionService auctionService;

        public AuctionController(IDbRepository<Auction> auctions,
            IDbRepository<Item> items,
            IDbRepository<User> users,
            IAuctionService auctionService)
        {
            this.dataAuction = auctions;
            this.dataItem = items;
            this.dataUser = users;
            this.auctionService = auctionService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateAuction()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuction(CreateAuctionModel auction)
        {
            if (this.ModelState.IsValid)
            {
                var item = this.dataItem.All().FirstOrDefault(i => i.Title == auction.ItemTitle);

                var items = new List<Item> { item };
                if (item == null)
                {
                    this.TempData["Success"] = "There is no such Item";

                    return this.View("CreateAuction", auction);
                }

                var bidders = this.dataUser.All().OrderBy(u => u.UserName).Take(3).ToList();

                //var newAuction = Mapper.Map<Auction>(auction);
                //this.dataAuction.Add(newAuction);


                this.dataAuction.Add(new Auction
                {
                    Name = auction.Name,
                    Items = items,
                    DateOfAuction = auction.DateOfAuction,
                    Bidders = bidders,
                    InitialPrice = auction.InitialPrice,
                    BidStep = auction.BidStep
                });


                this.dataAuction.Save();

                this.TempData["Success"] = "You have successfully created Auction";

                return this.RedirectToAction("CreateAuction");
            }

            return this.View("CreateAuction", auction);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult SetActiveAuction()
        {
            var pictureAuctions = this.auctionService.GroupByTypes(ItemType.Picture).ToList();
            ViewBag.PictureAuctions = pictureAuctions;

            var carAuctions = this.auctionService.GroupByTypes(ItemType.Car).ToList();
            ViewBag.CarAuctions = carAuctions;

            var coinAuctions = this.auctionService.GroupByTypes(ItemType.Coin).ToList();
            ViewBag.CoinAuctions = coinAuctions;

            var stampAuctions = this.auctionService.GroupByTypes(ItemType.PostageStamp).ToList();
            ViewBag.StampAuctions = stampAuctions;

            var statueAuctions = this.auctionService.GroupByTypes(ItemType.Statue).ToList();
            ViewBag.StatueAuctions = statueAuctions;

            var vaseAuctions = this.auctionService.GroupByTypes(ItemType.Vase).ToList();
            ViewBag.VaseAuctions = vaseAuctions;

            var otherAuctions = this.auctionService.GroupByTypes(ItemType.Other).ToList();
            ViewBag.OtherAuctions = otherAuctions;

            var model = new SetActiveAuctionModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult SetActiveAuction(int id)
        {
            var auction = this.dataAuction.GetById(id);

            if (auction == null)
            {
                this.TempData["Wrong"] = "There is no auction with that name and ID";

                return this.View("SetActiveAuction");
            }

            auction.Active = true;

            this.dataAuction.Save();

            var auctionView = new AuctionViewModel
            {
                Name = auction.Name,
                DateOfAuction = auction.DateOfAuction,
                Active = auction.Active,
                InitialPrice = auction.InitialPrice,
                BidStep = auction.BidStep,
            };

            return this.RedirectToAction("ListAllAuctions", "PublicAuction", auctionView);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditAuction(int id)
        {
            var auction = this.dataAuction.GetById(id);
            var model = this.Mapper.Map<CreateAuctionModel>(auction);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuction(CreateAuctionModel model)
        {
            if (this.ModelState.IsValid)
            {
                var auction = this.dataAuction.All().FirstOrDefault(a => a.Name == model.Name);

                if(auction == null)
                {
                    TempData["Failure"] = "There is no auction with that name!";

                    return this.View(model);
                }

                var itemsToRemove = auction.Items.ToList();
                foreach(var item in itemsToRemove)
                {
                    auction.Items.Remove(item);
                }

                var itemToAdd = this.dataItem.All().FirstOrDefault(i => i.Title == model.ItemTitle);
                if (itemToAdd == null)
                {
                    this.TempData["Failure"] = "There is no such Item";

                    return this.View(model);
                }

                auction.Name = model.Name;
                auction.Items.Add(itemToAdd);
                auction.DateOfAuction = model.DateOfAuction;
                auction.InitialPrice = model.InitialPrice;
                auction.BidStep = model.BidStep;

                this.dataAuction.Save();

                this.TempData["Success"] = "You have successfully updated the auction!";

                return this.RedirectToAction("ListAllAuctions", "PublicAuction", new { area = string.Empty });
            }

            return this.View(model);
        }
   
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var auction = this.dataAuction.GetById(id);
            this.dataAuction.Delete(auction);
            this.dataAuction.Save();

            this.TempData["Success"] = "You have successfully deleted the auction!";

            return this.RedirectToAction("ListAllAuctions", "PublicAuction", new { area = string.Empty });
        }
    }
}