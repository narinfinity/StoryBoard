namespace StoryBoard.Infrastructure.Data.Migrations.ApplicationDbContext
{
    using Core.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoryBoard.Infrastructure.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations.ApplicationDbContext";
        }

        protected override void Seed(StoryBoard.Infrastructure.Data.ApplicationDbContext context)
        {
            if (!context.Set<IdentityRole>().Any(r => r.Name == "Admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "Admin" };

                roleManager.Create(role);
                context.SaveChanges();
            }

            if (!context.Set<User>().Any(u => u.UserName == "Test"))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var newUser = new User { UserName = "test@test.com", Email = "test@test.com" };

                userManager.Create(newUser, "Test123#");
                userManager.AddToRole(newUser.Id, "Admin");
                context.SaveChanges();
            }

            var user = context.Set<User>().FirstOrDefault();
            if (user != null)
            {
                var story = new Story();
                story.Title = "The sample story";
                story.Description = "sample description";
                story.UserId = user.Id;
                story.User = user;
                story.Groups = new List<Group>();

                context.Entry(story).State = EntityState.Added;

                var group = new Group();
                group.Title = "The sample group";
                group.Description = "sample description";
                group.Stories = new List<Story>();
                group.Stories.Add(story);

                context.Entry(group).State = EntityState.Added;
                context.Set<Group>().Add(group);
                context.SaveChanges();
            }
            base.Seed(context);
        }
    }
}
