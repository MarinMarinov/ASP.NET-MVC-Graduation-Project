namespace Auction.Web.Controllers
{
    using System;
    using System.Linq;

    using Auction.Services.Data;
    using System.Web.Mvc;

    using Auction.Infrastructure.Mapping;
    using Auction.Web.ViewModels.Auction;

    public class HomeController : BaseController
    {
        private const int ItemsPerPage = 4;
        private IAuctionService auctions;

        public HomeController(IAuctionService service)
        {
            this.auctions = service;
        }

        public ActionResult Index(int id = 1)
        {
            var page = id;
            var totalItemsCount = this.auctions.GetAllAuctions().Count();
            var totalPages = (int)Math.Ceiling((double)totalItemsCount / (double)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;

            /* var auctionsModels = this.Cache.Get(
                 "auctions",
                 () =>
                 this.auctions.GetAllAuctions().Skip(itemsToSkip).Take(ItemsPerPage).To<ActiveAuctionViewModel>().ToList(),
                 20);*/

            var auctionsModels =
                this.auctions.GetAllAuctions()
                    .Skip(itemsToSkip)
                    .Take(ItemsPerPage)
                    .To<ActiveAuctionViewModel>()
                    .ToList();

            var listViewModel = new AuctionListViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Auctions = auctionsModels
            };

            return this.View(listViewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}