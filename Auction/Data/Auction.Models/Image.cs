namespace Auction.Models
{
    using Common;

    public class Image : BaseModel<int>
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public int ContentLength { get; set; }

        public byte[] ImageArray { get; set; }

        public int? ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
