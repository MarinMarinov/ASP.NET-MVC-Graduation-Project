namespace Auction.Web.Controllers
{
    using System.Web.Mvc;
    using Auction.Services.Data;
    using Auction.Web.ViewModels.Auction;
    using Infrastructure.Mapping;

    public class PublicAuctionController : BaseController
    {
        private readonly IAuctionService service;

        public PublicAuctionController(IAuctionService service)
        {
            this.service = service;
        }

        public ActionResult ListAllAuctions()
        {
            var auctions = this.service.GetAllAuctions().To<AuctionViewModel>();

            return this.View(auctions);
        }

        [Authorize]
        public ActionResult AuctionDetails(int id)
        {
            var auction = this.service.GetById(id);
            var model = this.Mapper.Map<AuctionDetailsViewModel>(auction);

            return View(model);
        }
    }
}