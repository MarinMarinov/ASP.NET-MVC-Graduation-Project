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

            return this.RedirectToAction("EditActiveAuction", "Auction", auctionView);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditActiveAuction(AuctionViewModel auctionView)
        {
            return this.View(auctionView);
        }
    }
}