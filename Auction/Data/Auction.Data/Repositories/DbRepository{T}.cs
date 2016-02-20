namespace Auction.Data.Repositories
{
    using Auction.Data;
    using Auction.Models.Common;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DbRepository<T> : IDbRepository<T>
        where T : class, IBaseModel
    {
        public DbRepository(IAuctionDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(
                    "An instance of DbContext is required to use this repository.");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; set; }

        private IAuctionDbContext Context { get; set; }

        public IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.DbSet;
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }

        public void HardDelete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }


        public T GetById(object id)
        {
            var entity = this.GetByIdWithDeleted(id);
            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }


        public T GetByIdWithDeleted(object id)
        {
            return this.DbSet.Find(id);
        }
    }
}
