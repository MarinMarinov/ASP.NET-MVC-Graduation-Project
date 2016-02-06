namespace Auction.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateAuctionModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfAuction { get; set; }
        
        [Required]
        public string ItemTitle { get; set; }
    }
}