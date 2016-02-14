namespace Auction.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Web.ViewModels.User;

    public class UsersController : BaseController
    {
        private IDbRepository<Auction, int> dataAuction;
        private IDbRepository<Item, int> dataItem;
        private IDbRepository<User, string> dataUser;

        public UsersController(IDbRepository<Auction, int> auctions, IDbRepository<Item, int> items, IDbRepository<User, string> users)
        {
            this.dataAuction = auctions;
            this.dataItem = items;
            this.dataUser = users;
        }

        [HttpGet]
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

            string userInfo = string.Format(
                "Username: {0}, Id: {1}, Phone number: {2}",
                user.UserName,
                user.Id,
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
        //  [OutputCache(Duration = 15 * 60)] // cache the result
        public ActionResult AllUsers()
        {
            var allUsers = this.dataUser.All()
                .Select(UserViewModel.FromUser).OrderBy(u => u.UserName).ToList();
            return View(allUsers);
        }

        public JsonResult AllUsersAsJson()
        {
            var users = this.dataUser.All().OrderBy(n => n.UserName)
                .Select(UserViewModel.FromUser).ToList();

            return this.Json(users, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public ActionResult CurrentUserDetails(HttpPostedFileBase file)
        {
            // absolute path: C:\Telerik\18.ASP.NET MVC\ASP.NET-MVC-Graduation-Project\Auction\Web\Auction.Infrastructure\Images\Avatars
            //var path = Path.Combine(Server.MapPath("~/Auction.Infrastructure/Images/Avatars/"), file.FileName);
            var path =
                "C:\\Telerik\\18.ASP.NET MVC\\ASP.NET-MVC-Graduation-Project\\Auction\\Web\\Auction.Infrastructure\\Images\\Avatars\\"
                + file.FileName;
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            using (var sw = new FileStream(path, FileMode.Create))
            {
                sw.Write(data, 0, data.Length);
            }

            var currentUserName = this.User.Identity.Name;

            var currentUser = this.dataUser.All()
                .FirstOrDefault(u => u.UserName == currentUserName);

            currentUser.AvatarLink = path;
            this.dataUser.Save();

            this.CurrentUser =
                this.dataUser.All()
                    .Where(u => u.UserName == currentUserName)
                    .Select(UserViewModel.FromUser)
                    .FirstOrDefault();


            return View(this.CurrentUser);
        }

    }
}