using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    using Auction.Services.Data;

    using Infrastructure;

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