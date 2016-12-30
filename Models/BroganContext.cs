using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Authentication;
using Models.BlogItems;

namespace Models
{
    public class BroganContext : IdentityDbContext<User, GuidRole ,Guid, GuidUserLogin, GuidUserRole, GuidUserClaim>
    {
        public DbSet<Blog> BlogPosts { get; set; }

        public BroganContext() : base("BroganContext")
        {

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
}
