using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using StoryBoard.Core.Entities;
using StoryBoard.Core.Interfaces;
using StoryBoard.Infrastructure.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace StoryBoard.Infrastructure.DependencyResolution
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers            

            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserStore<User>, UserStore<User>>(new HierarchicalLifetimeManager(), new InjectionConstructor(container.Resolve<DbContext>()));

            container.RegisterType<IDataContext, DataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IStoryRepository, StoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGroupRepository, GroupRepository>(new HierarchicalLifetimeManager());

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}