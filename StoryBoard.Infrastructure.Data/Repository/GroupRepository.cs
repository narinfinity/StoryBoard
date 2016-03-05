using StoryBoard.Core.Entities;
using StoryBoard.Core.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StoryBoard.Infrastructure.Data
{
    public class GroupRepository : RepositoryBase<Group, int>,
        IGroupRepository
    {
        public GroupRepository(IDataContext context)
            : base(context)
        { }

        public IQueryable<Group> GetGroupsWithStories(Expression<Func<Group, bool>> predicate = null)
        {
            var result = ((DbContext)this.Context).Set<Group>().Include(st => st.Stories);
            return predicate == null ? result : result.Where(predicate);
        }
        public Group GetGroup(int id, bool tracking = false)
        {
            var entity = GetGroupsWithStories(st => st.Id == id).FirstOrDefault();
            if (entity != null && !tracking)
                this.Context.Detach(entity);
            return entity;
        }

        public void AddGroup(Group group)
        {
            base.Create(group);
            base.Save();
        }

        public void UpdateGroup(Group group)
        {
            base.Update(group);
            base.Save();
        }
        public void DeleteGroup(int id)
        {
            base.Delete(id);
            base.Save();
        }
    }
}
