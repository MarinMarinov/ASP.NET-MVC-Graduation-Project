namespace Auction.Web.Controllers
{
    using Models;
    using System.Linq;
    using System.Web.Mvc;

    public class UserController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult UserDetails(string id)
        {
            var user =
                this.db.Users.FirstOrDefault(u => u.UserName == id);

            return View(user);
        }
            
    }
}