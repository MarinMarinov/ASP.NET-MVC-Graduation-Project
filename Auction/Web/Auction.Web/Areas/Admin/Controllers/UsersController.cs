namespace Auction.Web.Areas.Admin.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Web.Controllers;
    using Auction.Web.ViewModels.User;
    using Microsoft.AspNet.Identity;

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
        public ActionResult UserDetails(string id)
        {
            var user = this.dataUser.GetById(id);

            if(user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = this.Mapper.Map<UserViewModel>(user);

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CurrentUserDetails()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var currentUser = this.dataUser.GetById(currentUserId);
            var viewModel = this.Mapper.Map<UserViewModel>(currentUser);

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult CurrentUserDetails(UserViewModel model, HttpPostedFileBase file)
        {
            var viewModel = model;

            if (ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();
                var currentUser = this.dataUser.GetById(currentUserId);

                if (file != null)
                {
                    var path = Path.Combine(this.Server.MapPath("~/Content/Images/Avatars/"), file.FileName);

                    var data = new byte[file.ContentLength];
                    file.InputStream.Read(data, 0, file.ContentLength);
                    using (var sw = new FileStream(path, FileMode.Create))
                    {
                        sw.Write(data, 0, data.Length);
                    }

                    currentUser.AvatarFileName = file.FileName;
                }

                currentUser.UserName = model.UserName;
                currentUser.FirstName = model.FirstName;
                currentUser.LastName = model.LastName;
                currentUser.PhoneNumber = model.PhoneNumber;
                this.dataUser.Save();

                viewModel = this.Mapper.Map<UserViewModel>(currentUser);

                TempData["Success"] = "You have updated your details!";
            }

            return this.View(viewModel);
        }

        [Authorize]
        public ActionResult UserDetailsById(string id)
        {


            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            var user = this.dataUser.GetById(id);

            if (user == null)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.Content("User not found");
            }

            string userInfo = string.Format(
                "<i>Username:</i> <b>{0}</b>, <i>First name:</i> <b>{1}</b>, <i>Last name:</i> <b>{2}</b> <i>Phone number:</i> <b>{3}</b>",
                user.UserName,
                user.FirstName,
                user.LastName,
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
            return this.View(allUsers);
        }

        public FileResult GetImage(string filename)
        {
            var path = Path.Combine(this.Server.MapPath("~/Content/Images/Avatars/"), filename);

            return this.File(path, "image/jpeg");
        }

        public FileResult GetAvatar(string userId)
        {
            var user = this.dataUser.GetById(userId);
            var filename = user.AvatarFileName;
            if(filename == null)
            {
                return null;
            }
            var path = Path.Combine(this.Server.MapPath("~/Content/Images/Avatars/"), filename);

            return this.File(path, "image/jpeg");
        }
    }
}