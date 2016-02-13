namespace Auction.Data.Repositories
{
    using System.Linq;
    using Auction.Models.Common;

    /*public interface IDbRepository<T> : IDbRepository<T, int>
        where T : IBaseModel<int>
    {
    }*/

    public interface IDbRepository<T, in TKey>
        where T : class, IBaseModel<TKey>
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
