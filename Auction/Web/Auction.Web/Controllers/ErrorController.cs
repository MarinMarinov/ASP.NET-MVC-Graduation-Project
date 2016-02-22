namespace Auction.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : BaseController
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