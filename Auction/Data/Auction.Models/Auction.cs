﻿namespace Auction.Models
{
    using Models.Common;
    using System;
    using System.Collections.Generic;

    public class Auction : BaseModel
    {
        private ICollection<User> bidders;
        private ICollection<Item> items;

        public Auction()
        {
            this.bidders = new HashSet<User>();
            this.items = new HashSet<Item>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfAuction { get; set; }

        public bool Active { get; set; }

        public int InitialPrice { get; set; }

        public int BidStep { get; set; } 

        public string WinnerId { get; set; }

        public virtual User Winner { get; set; }

        public virtual ICollection<User> Bidders
        {
            get { return this.bidders; }
            set { this.bidders = value; }
        }

        public virtual ICollection<Item> Items
        {
            get { return this.items; }
            set { this.items = value; }
        }
    }
}
