namespace Auction.Models
{
    using System.ComponentModel.DataAnnotations;
    using global::Auction.Common.Models;

    public class Item : BaseModel<int>
    {
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
    }
}