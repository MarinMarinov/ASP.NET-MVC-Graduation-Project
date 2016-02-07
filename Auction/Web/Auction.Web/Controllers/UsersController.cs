namespace Auction.Web.Controllers
{
    using Models;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Auction.Models;
    using Data.Repositories;

    public class UsersController : BaseController
    {
        private IRepository<Auction> dataAuction;
        private IRepository<Item> dataItem;
        private IRepository<User> dataUser;

        public UsersController()
        {
            this.dataAuction = new EfGenericRepository<Auction>(this.DbContext);
            this.dataItem = new EfGenericRepository<Item>(this.DbContext);
            this.dataUser = new EfGenericRepository<User>(this.DbContext);
        }

        [Authorize]
        public ActionResult CurrentUserDetails()
        {
            var currentUserName = this.User.Identity.Name;

            this.CurrentUser = this.dataUser.All()
                .Where(u => u.UserName == currentUserName)
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
                this.dataUser.All()
                .Where(u => u.Id == id)
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
            var result = this.dataUser.All()
                .Where(u => u.UserName.ToLower().Contains(query.ToLower()))
                .Select(UserViewModel.FromUser)
                .ToList();

            return this.PartialView("_UsersSearchResults", result);
        }

        [Authorize]
        public ActionResult AllUsers()
        {
            var allUsers = this.dataUser.All()
                .Select(UserViewModel.FromUser).OrderBy(u => u.UserName).ToList();
            return View(allUsers);
        }

        public JsonResult AllUsersAsJson()
        {
            var users = this.dataUser.All()
            .Select(UserViewModel.FromUser);

            return this.Json(users, JsonRequestBehavior.AllowGet);
        }

    }
}