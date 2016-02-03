﻿using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    using Models;
    using System.Linq;
    using Infrastructure;

    public abstract class BaseController : Controller
    {
        private AuctionDbContext db;

        public BaseController()
        {
            this.DbContext = new AuctionDbContext();
            //GetUser();
        }

        /*private ActionResult GetUser()
        {
            if (this.User != null)
            {
                var currentUserName = this.User.Identity.Name;

                this.CurrentUser =
                    this.db.Users.FirstOrDefault(u => u.UserName == currentUserName);

                return RedirectToAction("UserDetails", "User");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }*/

        public User CurrentUser { get; set; }

        public AuctionDbContext DbContext
        {
            get { return this.db; }
            set { this.db = value; }
        }
    }
}