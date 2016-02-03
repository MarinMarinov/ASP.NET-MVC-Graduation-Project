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