using StoryBoard.Core.Entities;
using StoryBoard.Core.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StoryBoard.Infrastructure.Data
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntityIdentity<TKey>
    {
        protected IDataContext Context { get; private set; }

        public RepositoryBase(IDataContext context)
        {
            this.Context = context;
        }
        public IQueryable<TEntity> GetAll()
        {
            return this.Context.GetSet<TEntity>();
        }

        public TEntity Get(TKey id, bool tracking = false)
        {
            var entity = ((DbSet<TEntity>)this.Context.GetSet<TEntity>()).Find(id);
            if (entity != null && !tracking)
                this.Context.Detach(entity);
            return entity;
        }

        public void Update(TEntity entity)
        {
            this.Context.Attach<TEntity>(entity);
        }

        public virtual TEntity Create(TEntity entity = null)
        {
            return this.Context.Create(entity);
        }

        public void Save()
        {
            this.Context.Save();
        }

        public void Delete(TKey id)
        {
            var entity = this.Get(id, true);
            this.Context.Delete(entity);
        }
    }
}
