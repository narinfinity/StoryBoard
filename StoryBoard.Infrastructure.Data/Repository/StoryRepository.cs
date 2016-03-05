using StoryBoard.Core.Entities;
using StoryBoard.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StoryBoard.Infrastructure.Data
{
    public class StoryRepository : RepositoryBase<Story, int>,
        IStoryRepository
    {
        public StoryRepository(IDataContext context)
            : base(context)
        { }

        public IQueryable<Story> GetStoriesWithGroups(Expression<Func<Story, bool>> predicate = null)
        {
            var result = ((DbContext)this.Context).Set<Story>().Include(st => st.Groups).Include(st => st.User);
            return predicate == null? result : result.Where(predicate);
        }
        public Story GetStory(int id, bool tracking = false)
        {
            var entity = GetStoriesWithGroups(st => st.Id == id).FirstOrDefault();
            if (entity != null && !tracking)
                this.Context.Detach(entity);
            return entity;
        }
        
        public void AddStory(Story story)
        {                       
            base.Create(story);
            base.Save();
        }

        public void UpdateStory(Story story)
        {
            base.Update(story);
            base.Save();
        }
        public void DeleteStory(int id)
        {
            base.Delete(id);
            base.Save();
        }
    }
}
