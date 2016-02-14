namespace Auction.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Item : BaseModel<int>
    {
        private ICollection<Picture> pictures;

        public Item()
        {
            this.pictures = new HashSet<Picture>();
        }

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

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
    }
}
