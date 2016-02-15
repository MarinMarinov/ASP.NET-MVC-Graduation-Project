namespace Auction.Web.Controllers
{
    using System.Web.Mvc;
    using Auction.Services.Data;

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

        public ActionResult Test()
        {
            return View();
        }
    }
}