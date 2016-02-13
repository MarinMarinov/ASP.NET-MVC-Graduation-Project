namespace Auction.Web.Controllers
{
    using Auction.Data.Repositories;
    using Auction.Models;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class AuctionController : BaseController
    {
        private IDbRepository<Auction, int> dataAuction;
        private IDbRepository<Item, int> dataItem;
        private IDbRepository<User, string> dataUser;

        public AuctionController(IDbRepository<Auction, int> auctions,
            IDbRepository<Item, int> items,
            IDbRepository<User, string> users)
        {
            this.dataAuction = auctions;
            this.dataItem = items;
            this.dataUser = users;
        }

        //[Authorize(Roles="Admin")]
        // GET: Auction
        public ActionResult CreateAuction()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuction(CreateAuctionModel auction)
        {
            if (ModelState.IsValid)
            {
                //var item = this.DbContext.Items.FirstOrDefault(i => i.Title == auction.ItemTitle);
                var item = this.dataItem.All().FirstOrDefault(i => i.Title == auction.ItemTitle);

                var items = new List<Item> {item};
                if (item == null)
                {
                    TempData["Success"] = "There is no such Item";

                    return View("CreateAuction", auction);
                }
                //var currentUser = this.DbContext.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name );
                var currentUser = this.dataUser.All().FirstOrDefault(u => u.UserName == this.User.Identity.Name);

                //var bidders = this.DbContext.Users.OrderBy(u => u.UserName).Take(3).ToList();
                var bidders = this.dataUser.All().OrderBy(u => u.UserName).Take(3).ToList();

                this.dataAuction.Add(new Auction
                {
                    Name = auction.Name,
                    Items = items,
                    DateOfAuction = auction.DateOfAuction,
                    Creator = currentUser,
                    Bidders = bidders
                });

                this.dataAuction.Save();

                TempData["Success"] = "You have successfully created Auction";

                return RedirectToAction("CreateAuction");
            }

            return View("CreateAuction", auction);
        }
    }
}