namespace Auction.Models
{
    using Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Item : BaseModel
    {
        private ICollection<Image> images;

        public Item()
        {
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public ItemType Type { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        public int? AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
