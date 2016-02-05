namespace Auction.Web.Controllers
{
    using Models;
    using System.Linq;
    using System.Web.Mvc;

    public class UsersController : BaseController
    {
        [Authorize]
        public ActionResult UserDetails()
        {
            var currentUserName = this.User.Identity.Name;

            this.CurrentUser =
                this.DbContext.Users.Where(u => u.UserName == currentUserName)
                    .Select(UserViewModel.FromUser)
                    .FirstOrDefault();

            return View(this.CurrentUser);
        }

        public ActionResult AllUsers()
        {
            return this.View();
        }

        public JsonResult AllUsersAsJson()
        {
            var users = this.DbContext.Users.Select(UserViewModel.FromUser);

            return this.Json(users, JsonRequestBehavior.AllowGet);
        } 
            
    }
}