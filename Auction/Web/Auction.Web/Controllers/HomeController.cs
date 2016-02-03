using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    using Infrastructure;

    public class HomeController : BaseController
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