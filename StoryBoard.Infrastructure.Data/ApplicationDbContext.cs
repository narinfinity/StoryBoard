using Microsoft.AspNet.Identity.EntityFramework;
using StoryBoard.Core.Entities;
using System.Data.Entity;

namespace StoryBoard.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("IdentityDbConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Story>().ToTable("Stories");
            modelBuilder.Entity<Group>().ToTable("Groups");
            modelBuilder.Entity<User>().ToTable("AspNetUsers");

            modelBuilder.Entity<Story>()
                .HasRequired(c => c.User)
                .WithMany(u => u.Stories)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Story>()
                .HasMany(s => s.Groups)
                .WithMany(g => g.Stories)
                .Map(map => map.ToTable("GroupStories")
                .MapRightKey("GroupId")
                .MapLeftKey("StoryId"));

            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
