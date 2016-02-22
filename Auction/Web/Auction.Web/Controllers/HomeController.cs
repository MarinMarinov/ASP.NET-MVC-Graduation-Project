namespace Auction.Web.Controllers
{
    using Auction.Services.Data;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        private IAuctionService service;

        public HomeController(IAuctionService addService)
        {
            this.service = addService;
        }

        public ActionResult Index()
        {
            return View();
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