using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Services.Data;
    using Auction.Web.ViewModels.Auction;
    using Infrastructure.Mapping;
    public class PublicAuctionController : BaseController
    {
                private IDbRepository<Auction> dataAuction;
        private IDbRepository<Item> dataItem;
        private IDbRepository<User> dataUser;
        private IAuctionService service;

        public PublicAuctionController(IDbRepository<Auction> auctions,
            IDbRepository<Item> items,
            IDbRepository<User> users,
            IAuctionService service)
        {
            this.dataAuction = auctions;
            this.dataItem = items;
            this.dataUser = users;
            this.service = service;
        }

        public ActionResult ListAllAuctions()
        {
            var auctions = this.service.GetAllAuctions().To<AuctionViewModel>();

            return this.View(auctions);
        }

      
    }
}