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
        private IAuctionService service;

        public AuctionController(IDbRepository<Auction> auctions,
            IDbRepository<Item> items,
            IDbRepository<User> users,
            IAuctionService service)
        {
            this.dataAuction = auctions;
            this.dataItem = items;
            this.dataUser = users;
            this.service = service;
        }

        //[Authorize(Roles="Admin")]
        // GET: Auction
        public ActionResult CreateAuction()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuction(CreateAuctionModel auction)
        {
            if (this.ModelState.IsValid)
            {
                var item = this.dataItem.All().FirstOrDefault(i => i.Title == auction.ItemTitle);

                var items = new List<Item> {item};
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
        public ActionResult SetActiveAuction()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult SetActiveAuction(string auctionName)
        {
            var auction = this.dataAuction.All().FirstOrDefault(a => a.Name == auctionName);

            if (auction == null)
            {
                this.TempData["Wrong"] = "There is no auction with that name";

                return this.View("SetActiveAuction");
            }

            auction.Active = true;

            this.dataAuction.Save();

            var auctionView = new AuctionViewModel 
            { 
                Name = auction.Name, 
                DateOfAuction=auction.DateOfAuction,
                Active = auction.Active,
                InitialPrice = auction.InitialPrice,
                BidStep = auction.BidStep,
                //Creator = auction.Creator.UserName
            };

            return this.RedirectToAction("EditActiveAuction", "Auction", auctionView);
        }

        // [Authorize(Role="Admin)]
        public ActionResult EditActiveAuction(AuctionViewModel auctionView)
        {
            return this.View(auctionView);
        }
    }
}