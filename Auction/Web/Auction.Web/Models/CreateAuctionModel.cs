namespace Auction.Web.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Auction.Models;
    using AutoMapper;
    using Infrastructure.Mappings;

    public class CreateAuctionModel : IMapFrom<Auction>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Item Title")]
        public string ItemTitle { get; set; }

        [Required]
        [DisplayName("Auction Date and Time")]
        public DateTime DateOfAuction { get; set; }
        
        
    }
}