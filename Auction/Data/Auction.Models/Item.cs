namespace Auction.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Item
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