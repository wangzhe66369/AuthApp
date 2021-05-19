using AuthApp.Domian;
using Microsoft.EntityFrameworkCore;
using AuthApp.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AuthApp.Authorization.Users;
using AuthApp.Authorization.Roles;

namespace AuthApp.EntityFrameworkCore
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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