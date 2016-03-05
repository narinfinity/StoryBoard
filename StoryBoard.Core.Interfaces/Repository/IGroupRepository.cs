using StoryBoard.Core.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace StoryBoard.Core.Interfaces
{
    public interface IGroupRepository : IRepository<Group, int>
    {
        IQueryable<Group> GetGroupsWithStories(Expression<Func<Group, bool>> predicate = null);
        Group GetGroup(int id, bool tracking = false);
        void AddGroup(Group story);
        void UpdateGroup(Group story);
        void DeleteGroup(int id);
    }
}
