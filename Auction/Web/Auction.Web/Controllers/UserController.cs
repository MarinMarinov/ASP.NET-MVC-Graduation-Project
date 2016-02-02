namespace Auction.Web.Controllers
{
    using Models;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using System.Web;
    using System.Web.Mvc;

    public class UserController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult UserDetails()
        {
            var currentUserName = this.User.Identity.Name;
            var user =
                this.db.Users.FirstOrDefault(u => u.UserName == currentUserName);

            return View(user);
        }
            
    }
}