using StoryBoard.Core.Entities;
using StoryBoard.Core.Interfaces;
using StoryBoard.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Filters;

namespace StoryBoard.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IStoryRepository _storyStore;
        IGroupRepository _groupStore;
        ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public HomeController(IStoryRepository storyStore, IGroupRepository groupStore)
        {
            _storyStore = storyStore;
            _groupStore = groupStore;
        }

        //Stories
        public ActionResult Index()
        {
            return RedirectToAction("StoryList");
        }

        public ActionResult StoryList()
        {
            return View("StoryList");
        }

        [HttpGet]
        [ModelStateToTempData]
        public ActionResult AddEditStory(int? storyId)
        {
            var model = new StoryViewModel { Id = 0 };
            if (storyId.HasValue && storyId.Value > 0)
            {
                var story = _storyStore.GetStoriesWithGroups(s => s.Id == storyId.Value).FirstOrDefault();
                if (story != null)
                {
                    model.Id = story.Id;
                    model.Title = story.Title;
                    model.Description = story.Description;
                    model.Content = story.Content;
                    model.Groups = new List<GroupViewModel>((story.Groups ?? new List<Group>()).Select(g => new GroupViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Description = g.Description,
                        Content = g.Content ?? string.Empty,
                        Stories = null
                    }));
                    model.GroupId = model.Groups.Count > 0 ? model.Groups.First().Id : 0;
                }
            }

            return View("_AddEditStory", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        public ActionResult SaveStory(StoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddEditStory", "Home", new { storyId = model.Id });
            }
            var userId = User.Identity.GetUserId();
            var group = _groupStore.Get(model.GroupId, true);

            if (model.Id > 0)
            {
                var storyExistent = _storyStore.GetStory(model.Id, true);
                if (storyExistent != null)
                {
                    storyExistent.Id = model.Id;
                    storyExistent.Title = model.Title;
                    storyExistent.Description = model.Description;
                    storyExistent.Content = model.Content;
                    storyExistent.UserId = userId;
                    if (storyExistent.Groups == null)
                        storyExistent.Groups = new List<Group>();

                    if (!storyExistent.Groups.Any(g => g.Id == model.GroupId))
                        storyExistent.Groups.Add(group);
                    _storyStore.UpdateStory(storyExistent);
                }
            }
            else
            {
                var story = new Story
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    UserId = userId,
                    Groups = new List<Group>()
                };
                story.Groups.Add(group);
                _storyStore.AddStory(story);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteStory(int? storyId)
        {
            if (storyId.HasValue && storyId.Value > 0)
            {
                var storyExistent = _storyStore.GetStory(storyId.Value);
                if (storyExistent != null)
                {
                    _storyStore.DeleteStory(storyId.Value);
                }
            }
            return RedirectToAction("StoryList", "Home");
        }



        //Groups
        public ActionResult GroupList()
        {
            return View("GroupList");
        }

        [HttpGet]
        [ModelStateToTempData]
        public ActionResult AddEditGroup(int? groupId)
        {
            var model = new GroupViewModel { Id = 0 };
            if (groupId.HasValue && groupId.Value > 0)
            {
                var group = _groupStore.GetGroupsWithStories(s => s.Id == groupId.Value).FirstOrDefault();
                if (group != null)
                {
                    model.Id = group.Id;
                    model.Title = group.Title;
                    model.Description = group.Description;
                    model.Content = group.Content;
                    model.Stories = new List<StoryViewModel>((group.Stories ?? new List<Story>()).Select(s => new StoryViewModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        Content = s.Content ?? string.Empty,
                        Groups = null
                    }));
                }
            }

            return View("_AddEditGroup", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        public ActionResult SaveGroup(GroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddEditGroup", "Home", new { groupId = model.Id });
            }
            var userId = User.Identity.GetUserId();
            

            if (model.Id > 0)
            {
                var groupExistent = _groupStore.GetGroup(model.Id, true);
                if (groupExistent != null)
                {
                    groupExistent.Id = model.Id;
                    groupExistent.Title = model.Title;
                    groupExistent.Description = model.Description;
                    groupExistent.Content = model.Content;
                    if (groupExistent.Stories == null)
                        groupExistent.Stories = new List<Story>();                    
                    _groupStore.UpdateGroup(groupExistent);
                }
            }
            else
            {
                var group = new Group
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Stories = new List<Story>()
                };
                _groupStore.AddGroup(group);
            }

            return RedirectToAction("GroupList", "Home");
        }

        public ActionResult DeleteGroup(int? groupId)
        {
            if (groupId.HasValue && groupId.Value > 0)
            {
                var groupExistent = _groupStore.GetGroup(groupId.Value);
                if (groupExistent != null)
                {                    
                    _groupStore.DeleteGroup(groupId.Value);
                }
            }
            return RedirectToAction("GroupList", "Home");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}