namespace Auction.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Auction.Models;
    using AutoMapper;
    using Infrastructure.Mappings;

    public class CreateAuctionModel : IMapFrom<Auction>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfAuction { get; set; }
        
        [Required]
        public string ItemTitle { get; set; }
    }
}