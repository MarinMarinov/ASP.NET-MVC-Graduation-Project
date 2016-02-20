namespace Auction.Data.Repositories
{
    using Auction.Models.Common;
    using System.Linq;

    public interface IDbRepository<T>
        where T : class, IBaseModel
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(object id);

        T GetByIdWithDeleted(object id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
