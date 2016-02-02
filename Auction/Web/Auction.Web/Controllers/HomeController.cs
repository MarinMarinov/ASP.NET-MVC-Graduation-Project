using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    public class HomeController : Controller
    {
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