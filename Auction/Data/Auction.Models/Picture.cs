namespace Auction.Models
{
    using Common;

    public class Picture : BaseModel<int>
    {
        public string FileName { get; set; }

        public byte[] Image { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
