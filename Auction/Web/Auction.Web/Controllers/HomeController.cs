using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    using Infrastructure;

    public class HomeController : Controller
    {
        public HomeController(IAuctionService addService)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Application for managing on-line and live auctions.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}