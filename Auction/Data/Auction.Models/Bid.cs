namespace Auction.Models
{
    using Common;
    using System.Collections.Generic;

    public class Bid : BaseModel
    {
        private ICollection<User> bidders;

        public Bid()
        {
            this.bidders = new HashSet<User>();
        }

        public int Id { get; set; }

        public int Value { get; set; }

        public int AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        public int? NewPrice { get; set; }

        public string WinnerId { get; set; }

        public virtual User Winner { get; set; }

        public string WinnerUsername { get; set; }

        public string BidderId { get; set; }

        public virtual User Bidder { get; set; }

        public virtual ICollection<User> Bidders
        {
            get { return this.bidders; }
            set { this.bidders = value; }
        }
    }
}
