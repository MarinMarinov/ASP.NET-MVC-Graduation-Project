﻿namespace Auction.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Item : BaseModel<int>
    {
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