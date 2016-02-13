namespace Auction.Common.Repositories
{
    using System.Linq;
    using Auction.Common.Models;

    public interface IDbRepository<T> : IDbRepository<T, int>
        where T : IBaseModel<int>
    {
    }

    public interface IDbRepository<T, in TKey>
        where T : IBaseModel<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(TKey id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
