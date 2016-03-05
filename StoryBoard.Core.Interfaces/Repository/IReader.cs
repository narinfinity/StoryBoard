using StoryBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Core.Interfaces
{
    public interface IReader<TEntity, TKey>
       where TEntity : class, IEntityIdentity<TKey>
    {
        TEntity Get(TKey id, bool tracking = false);
        IQueryable<TEntity> GetAll();
    }
}
