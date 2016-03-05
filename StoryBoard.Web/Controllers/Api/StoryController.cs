using StoryBoard.Core.Entities;
using StoryBoard.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace StoryBoard.Web.Controllers
{
    
    public class StoryController : ApiController
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

        public StoryController(IStoryRepository storyStore, IGroupRepository groupStore)
        {
            _storyStore = storyStore;
            _groupStore = groupStore;
        }
        // GET: api/Story
        public IEnumerable<Story> Get(int? groupId)
        {
            var loggedInUserId = User.Identity.GetUserId();
            var stories = !string.IsNullOrEmpty(loggedInUserId) 
                ? _storyStore.GetStoriesWithGroups(s=>s.UserId == loggedInUserId)
                .Where(s => (groupId == null || s.Groups.Any(g => g.Id == groupId)))
                .OrderByDescending(s => s.Id)
                .ToList()
                :new List<Story>();
            stories.ForEach(s => { s.Groups.ForEach(g => { g.Stories = null; }); s.User = null; });
            return stories;
        }

        // GET: api/Story/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Story
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Story/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Story/5
        public void Delete(int id)
        {
        }
    }
}
