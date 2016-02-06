namespace Auction.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Auction
    {
        private ICollection<User> bidders;

        public Auction()
        {
            this.bidders = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime DateOfAuction { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public string CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public virtual ICollection<User> Bidders
        {
            get { return this.bidders; }
            set { this.bidders = value; }
        }
    }
}