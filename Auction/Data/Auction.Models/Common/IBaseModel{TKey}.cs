namespace Auction.Models.Common
{

    public interface IBaseModel<TKey> : IAuditInfo, IDeletableEntity
    {
        TKey Id { get; set; }
    }
}
