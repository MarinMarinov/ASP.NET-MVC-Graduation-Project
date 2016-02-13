namespace Auction.Common.Models
{
    public interface IBaseModel<TKey> : IAuditInfo, IDeletableEntity
    {
        TKey Id { get; set; }
    }
}
