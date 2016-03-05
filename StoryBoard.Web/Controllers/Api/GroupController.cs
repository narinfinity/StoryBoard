using StoryBoard.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using StoryBoard.Core.Entities;
using Microsoft.AspNet.Identity;

namespace StoryBoard.Web.Controllers
{
   
    public class GroupController : ApiController
    {
        IStoryRepository _storyStore;
        IGroupRepository _groupStore;
        ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public GroupController(IStoryRepository storyStore, IGroupRepository groupStore)
        {
            _storyStore = storyStore;
            _groupStore = groupStore;           
        }
        // GET: api/Group
        public IEnumerable<Group> Get(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUserId = User.Identity.GetUserId();
                var stories = !string.IsNullOrEmpty(loggedInUserId)
                    ? _storyStore.GetStoriesWithGroups(s => s.UserId == loggedInUserId).OrderByDescending(s => s.Id).ToList() 
                    : new List<Story>();
                var groups = new List<Group>();
                stories.ForEach(s => s.Groups.ForEach(g => { g.Stories = null; if (!groups.Any(gr => gr.Id == g.Id)) { groups.Add(g); } }));
                return groups;
            }
            var allGroups = _groupStore.GetGroupsWithStories().OrderByDescending(g => g.Id).ToList();
            allGroups.ForEach(g => g.Stories.ForEach(s => s.Groups = null));
            return allGroups;
        }

        // GET: api/Group/5
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Group
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Group/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Group/5
        public void Delete(int id)
        {
        }
    }
}
