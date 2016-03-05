using StoryBoard.Core.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace StoryBoard.Core.Interfaces
{
    public interface IStoryRepository : IRepository<Story, int>
    {
        IQueryable<Story> GetStoriesWithGroups(Expression<Func<Story, bool>> predicate = null);
        Story GetStory(int id, bool tracking = false);
        void AddStory(Story story);
        void UpdateStory(Story story);
        void DeleteStory(int id);
    }
}
