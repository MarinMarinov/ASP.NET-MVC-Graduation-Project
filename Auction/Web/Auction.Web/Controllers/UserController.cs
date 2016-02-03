namespace Auction.Web.Controllers
{
    using Models;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using System.Web;
    using System.Web.Mvc;

    public class UserController : BaseController
    {
        [Authorize]
        public ActionResult UserDetails()
        {
            var currentUserName = this.User.Identity.Name;

                this.CurrentUser =
                    this.DbContext.Users.FirstOrDefault(u => u.UserName == currentUserName);

            return View(this.CurrentUser);
        }
            
    }
}