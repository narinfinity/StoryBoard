
using StoryBoard.Core.Entities;

namespace StoryBoard.Core.Interfaces
{
    public interface IRepository<TEntity, TKey> : IReader<TEntity, TKey>
    where TEntity : class, IEntityIdentity<TKey>
    {
        void Update(TEntity entity);
        TEntity Create(TEntity entity = null);
        void Save();
        void Delete(TKey id);
    }
}
