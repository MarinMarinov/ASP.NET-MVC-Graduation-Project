namespace Auction.Web.ViewModels.Item
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Auction.Infrastructure.Mapping;

    using Models;

    public class CreateItemViewModel : IMapFrom<Item>
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