using System.Linq;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Web.ViewModels.Auction;
    using Auction.Web.ViewModels.Bid;

    using Microsoft.AspNet.Identity;

    public class BidController : Controller
    {
        private IDbRepository<Auction> dataAuction;
        private IDbRepository<Bid> dataBid;
        private IDbRepository<User> dataUser;
        private IDbRepository<Image> dataImage;

        public BidController(IDbRepository<Auction> auctions, IDbRepository<Bid> bids, IDbRepository<User> users, IDbRepository<Image> dataImage)
        {
            this.dataAuction = auctions;
            this.dataBid = bids;
            this.dataUser = users;
            this.dataImage = dataImage;
        }

        [HttpGet]
        public ActionResult JoinToAuction()
        {
            var activeAuctions =
                dataAuction.All()
                    .Where(auction => auction.Active)
                    .Select(
                        auction =>
                        new JoinToActiveAuctionViewModel
                            {
                                Id = auction.Id,
                                Name = auction.Name,
                                InitialPrice = auction.InitialPrice,
                                BidStep = auction.BidStep,
                                DateOfAuction = auction.DateOfAuction,
                                Active = auction.Active,
                                Items = auction.Items
                            }).ToList();

            return View(activeAuctions);
        }

        [HttpGet]
        [Authorize]
        public ActionResult JoinToAuctionId(int auctionId)
        {
            var auction = this.dataAuction.GetById(auctionId);
            var currentUserId = this.User.Identity.GetUserId();

            var currentUser = this.dataUser.GetById(currentUserId);

            auction.Bidders.Add(currentUser);
            this.dataAuction.Save();

            var auctionModel = new ActiveAuctionViewModel
                            {
                                Id = auction.Id,
                                Name = auction.Name,
                                Value = auction.BidStep,
                                InitialPrice = auction.InitialPrice,
                                BidStep = auction.BidStep,
                                Active = auction.Active,
                                Items = auction.Items,
                                Bidders = auction.Bidders
                            };

            var bidders = auction.Bidders
                .Select(u => new SelectListItem
                {
                    Text = u.UserName,
                    Value = u.Id
                })
                .ToList();
            bidders.Add(new SelectListItem { Text = "All", Value = "All" });
            ViewBag.Bidders = bidders;

            return View("Bid", auctionModel);
        }

        // Just for client-side validation
        public void SendMessage(ActiveAuctionViewModel auction)
        {
        }

        public ActionResult LoadChatHistory()
        {
            var userId = this.User.Identity.GetUserId();

            var bidds =
                this.dataBid.All()
                    .Where(m => m.BidderId == userId || m.Bidders.Any(u => u.Id == userId))
                    .Select(b => new BidDetailsViewModel{
                        Value = b.Value,
                        NewPrice = b.NewPrice,
                        CreatedOn = b.CreatedOn,
                        Bidder = b.Bidder,
                        Bidders = b.Bidders,
                        Winner = b.Winner
                    })
                    .OrderByDescending(b => b.CreatedOn)
                    .ToList();

            return PartialView("_ChatHistory", bidds);
        }

    }
}