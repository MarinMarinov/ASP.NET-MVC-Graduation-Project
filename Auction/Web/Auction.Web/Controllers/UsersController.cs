namespace Auction.Web.Controllers
{
    using Models;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class UsersController : BaseController
    {
        [Authorize]
        public ActionResult CurrentUserDetails()
        {
            var currentUserName = this.User.Identity.Name;

            this.CurrentUser =
                this.DbContext.Users.Where(u => u.UserName == currentUserName)
                    .Select(UserViewModel.FromUser)
                    .FirstOrDefault();

            return View(this.CurrentUser);
        }

        [Authorize]
        public ActionResult UserDetailsById(string id)
        {
            

            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            var user =
                this.DbContext.Users.Where(u => u.Id == id)
                    .Select(UserViewModel.FromUser)
                    .FirstOrDefault();

            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.Content("User not found");
            }

            string userInfo = string.Format("Username: {0}, Id: {1}, Phone number: {2}", user.UserName, user.Id,
               user.PhoneNumber);
            
            return this.Content(userInfo);
        }

        [Authorize]
        public ActionResult Search(string query)
        {
            var result = this.DbContext.Users
                .Where(u => u.UserName.ToLower().Contains(query.ToLower()))
                .Select(UserViewModel.FromUser)
                .ToList();

            return this.PartialView("_UsersSearchResults", result);
        }

        [Authorize]
        public ActionResult AllUsers()
        {
            var allUsers = this.DbContext.Users.Select(UserViewModel.FromUser).OrderBy(u => u.UserName).ToList();
            return View(allUsers);
        }

        public JsonResult AllUsersAsJson()
        {
            var users = this.DbContext.Users.Select(UserViewModel.FromUser);

            return this.Json(users, JsonRequestBehavior.AllowGet);
        } 
            
    }
}