namespace Auction.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Auction.Data.Repositories;
    using Auction.Models;
    using Auction.Web.Controllers;

    public class ImageController : BaseController
    {
        private IDbRepository<Image> dataImage;

        public ImageController(
            IDbRepository<Image> images)
        {
            this.dataImage = images;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult DeleteImage(int imageId, int itemId)
        {
            var image = this.dataImage.GetById(imageId);
            this.dataImage.HardDelete(image);
            this.dataImage.Save();

            TempData["Success"] = "You have successfully deleted the image";

            return this.RedirectToAction("Edit", "Item", new {id = itemId}); // TODO must return Id
        }
    }
}