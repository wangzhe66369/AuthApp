using AuthApp.Domian;
using Microsoft.EntityFrameworkCore;
using AuthApp.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AuthApp.Identity.Users;
using AuthApp.Identity.Roles;

namespace AuthApp.EntityFrameworkCore
{
    public class DbContextBase : IdentityDbContext<User, Role, int>
    {
        public DbContextBase(DbContextOptions<DbContextBase> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=LAPTOP-QDDHF04P;Database=MigrationCoreContext;uid=sa;pwd=sa123");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}