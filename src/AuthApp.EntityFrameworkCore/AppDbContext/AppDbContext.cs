using AuthApp.Domian;
using Microsoft.EntityFrameworkCore;
using AuthApp.Extensions;
using AuthApp.EntityFrameworkCore.EntityTypeConfiguration;
using VCrisp.Infrastructure.Core;
using AuthApp.Domian.Identity;
using AuthApp.Domian.Auth;

namespace AuthApp.EntityFrameworkCore
{
    public class AppDbContext : EFContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //public DbSet<Author> Authors { get; set; }

        //public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        

        public DbSet<ModulePermission> ModulePermissions { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RoleModulePermission> RoleModulePermissions { get; set; }


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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
            modelBuilder.Seed();
        }
    }
}