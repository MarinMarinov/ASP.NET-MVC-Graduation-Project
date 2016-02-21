namespace Auction.Web.ViewModels.Bid
{
    using System.ComponentModel.DataAnnotations;

    public class BidCreateModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Value { get; set; }

        [Required]
        public string ReceiverId { get; set; }
    }
}