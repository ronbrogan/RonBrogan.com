using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Authentication;
using Models.BlogItems;
using EFCache;
using System.Data.Entity.Core.Common;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;

namespace Models
{
    public class BroganContext : IdentityDbContext<User, GuidRole, Guid, GuidUserLogin, GuidUserRole, GuidUserClaim>
    {
        public DbSet<Blog> BlogPosts { get; set; }

        public BroganContext() : base("BroganContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BroganContext, Models.Migrations.Configuration>());
            Database.Initialize(false);
        }

        public static BroganContext Create()
        {
            return new BroganContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<GuidUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<GuidRole>().ToTable("Roles");
            modelBuilder.Entity<GuidUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<GuidUserClaim>().ToTable("UserClaims");

        }

    }

    public class Configuration : DbConfiguration
    {
        public Configuration()
        {
            var transactionHandler = new CacheTransactionHandler(new InMemoryCache());

            AddInterceptor(transactionHandler);

            Loaded +=
              (sender, args) => args.ReplaceService<DbProviderServices>(
                (s, _) => new CachingProviderServices(s, transactionHandler,
                  new DefaultCachingPolicy()));
        }
    }

    public class DefaultCachingPolicy : CachingPolicy
    {
        public static readonly List<string> DefaultCacheableEntities = new List<string>
        {
            "Blogs"
        };

        protected override void GetExpirationTimeout(ReadOnlyCollection<EntitySetBase> affectedEntitySets, out TimeSpan slidingExpiration, out DateTimeOffset absoluteExpiration)
        {
            slidingExpiration = TimeSpan.FromMinutes(5);
            absoluteExpiration = DateTimeOffset.Now.AddMinutes(30);
        }

        protected override bool CanBeCached(ReadOnlyCollection<EntitySetBase> affectedEntitySets, string sql, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return affectedEntitySets.All(s => DefaultCacheableEntities.Any(c => c == s.Table));
        }

    }
}
