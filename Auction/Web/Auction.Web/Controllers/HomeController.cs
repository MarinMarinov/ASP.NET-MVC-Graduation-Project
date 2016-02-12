using System.Web.Mvc;

namespace Auction.Web.Controllers
{
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
            this.service.Work(); // to test the Autofac dependency container
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