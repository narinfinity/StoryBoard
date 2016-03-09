using Microsoft.AspNet.Identity.EntityFramework;
using StoryBoard.Core.Entities;
using StoryBoard.Core.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace StoryBoard.Infrastructure.Data
{
    public class DataContext : DbContext, IDataContext
    {
        static DataContext() { Database.SetInitializer<DataContext>(null); }
        public DataContext()
            : base("IdentityDbConnection")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            Configuration.UseDatabaseNullSemantics = false;
        }

        public DbSet<Story> Stories { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Story>().ToTable("Stories");
            modelBuilder.Entity<Group>().ToTable("Groups");           
            modelBuilder.Entity<User>().ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");

            modelBuilder.Entity<Story>()
                .HasRequired(c => c.User)                
                .WithMany(u => u.Stories)
                .HasForeignKey(s=>s.UserId)                                                              
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

        public IQueryable<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            this.Set<TEntity>().Attach(entity);
            base.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            if (this.Set<TEntity>().Local.Contains(entity))
                ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public TEntity Create<TEntity>(TEntity entity = null) where TEntity : class
        {
            if (entity != null)
            {
                this.Set<TEntity>().Attach(entity);
                base.Entry<TEntity>(entity).State = EntityState.Added;
            }
            else
                entity = this.Set<TEntity>().Create();

            return entity;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            base.SaveChanges();
        }

        
    }
}
