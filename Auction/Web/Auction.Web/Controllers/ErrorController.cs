namespace Auction.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}