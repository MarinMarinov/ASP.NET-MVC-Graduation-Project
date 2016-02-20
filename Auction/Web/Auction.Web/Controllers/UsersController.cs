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
        private IDbRepository<Auction> dataAuction;
        private IDbRepository<Item> dataItem;
        private IDbRepository<User> dataUser;

        public UsersController(IDbRepository<Auction> auctions, IDbRepository<Item> items, IDbRepository<User> users)
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
            var path = Path.Combine(Server.MapPath("~/Content/Images/Avatars/"), file.FileName);
            
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            using (var sw = new FileStream(path, FileMode.Create))
            {
                sw.Write(data, 0, data.Length);
            }

            var currentUserName = this.User.Identity.Name;

            var currentUser = this.dataUser.All()
                .FirstOrDefault(u => u.UserName == currentUserName);

            currentUser.AvatarFileName = file.FileName;
            this.dataUser.Save();

            this.CurrentUser =
                this.dataUser.All()
                    .Where(u => u.UserName == currentUserName)
                    .Select(UserViewModel.FromUser)
                    .FirstOrDefault();


            return View(this.CurrentUser);
        }

        public FileResult GetImage(string filename)
        {
            var currentUserName = this.User.Identity.Name;

            this.CurrentUser =
                this.dataUser.All()
                    .Where(u => u.UserName == currentUserName)
                    .Select(UserViewModel.FromUser)
                    .FirstOrDefault();

            var path = Path.Combine(Server.MapPath("~/Content/Images/Avatars/"), filename);

            return File(path, "image/jpeg");

            /*byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = System.IO.Path.GetFileName(path);
            string extension = System.IO.Path.GetExtension(path);
            return File(fileBytes, extension, fileName);*/
        }

    }
}