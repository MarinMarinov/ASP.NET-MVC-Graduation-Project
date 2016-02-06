
namespace Auction.Web.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Type Type { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int AuctionId { get; set; }

        public virtual Auction Auction { get; set; }
    }
}