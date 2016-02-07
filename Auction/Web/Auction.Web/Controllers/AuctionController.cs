namespace Auction.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Auction.Models;
    using Data.Repositories;
    using Models;

    public class AuctionController : BaseController
    {
        private IRepository<Auction> dataAuction;
        private IRepository<Item> dataItem;
        private IRepository<User> dataUser;

        public AuctionController()
        {
            this.dataAuction = new EfGenericRepository<Auction>(this.DbContext);
            this.dataItem = new EfGenericRepository<Item>(this.DbContext);
            this.dataUser = new EfGenericRepository<User>(this.DbContext);
        }

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
                    DateOfCreation = DateTime.UtcNow,
                    DateOfAuction = auction.DateOfAuction,
                    Creator = currentUser,
                    Bidders = bidders
                });

                this.dataAuction.SaveChanges();

                TempData["Success"] = "You have successfully created Auction";

                return RedirectToAction("CreateAuction");
            }

            return View("CreateAuction", auction);
        }
    }
}