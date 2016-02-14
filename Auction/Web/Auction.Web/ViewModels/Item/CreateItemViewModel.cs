namespace Auction.Web.ViewModels.Item
{
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class CreateItemViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public ItemType Type { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        public string PictureName { get; set; }
    }
}