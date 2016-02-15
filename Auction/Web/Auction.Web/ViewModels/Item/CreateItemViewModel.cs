namespace Auction.Web.ViewModels.Item
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class CreateItemViewModel
    {
        public CreateItemViewModel()
        {
            this.Images = new List<Image>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public ItemType Type { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ICollection<Image> Images { get; set; }
    }
}