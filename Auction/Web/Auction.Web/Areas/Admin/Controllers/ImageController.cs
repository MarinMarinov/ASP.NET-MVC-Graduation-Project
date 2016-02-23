using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Areas.Admin.Controllers
{
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Services.Data;

    public class ImageController : Controller
    {
        private IDbRepository<Image> dataImage;

        public ImageController(
            IDbRepository<Image> images)
        {
            this.dataImage = images;
        }

        [HttpGet]
        public ActionResult DeleteImage(int imageId, int itemId)
        {
            var image = this.dataImage.GetById(imageId);
            this.dataImage.HardDelete(image);
            this.dataImage.Save();

            /*if (Request.IsAjaxRequest())
            {
                //return this.Json(new { id = image.Id });
                return Content("It IS AJAX!");
            }
            else
            {
                return this.Json(new { id = image.Id });
            }*/

            TempData["Success"] = "You have successfully deleted the image";

            return this.RedirectToAction("Edit", "Item", new {id = itemId}); // TODO must return Id
        }
    }
}