namespace Auction.Web.Controllers
{
    using System.Web.Mvc;
    using Auction.Web.ViewModels.User;

    public abstract class BaseController : Controller
    {
       

        /*private ActionResult GetUser()
        {
            if (this.User != null)
            {
                var currentUserName = this.User.Identity.Name;

                this.CurrentUser =
                    this.db.Users.FirstOrDefault(u => u.UserName == currentUserName);

                return RedirectToAction("UserDetails", "User");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }*/

        public UserViewModel CurrentUser { get; set; }

       
    }
}