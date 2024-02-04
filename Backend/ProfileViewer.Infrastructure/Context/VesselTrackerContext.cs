using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Infrastructure.Configurations;

namespace ProfileViewer.Infrastructure.Context
{
    public class ProfileViewerContext(DbContextOptions<ProfileViewerContext> options) : IdentityDbContext<User, Role, Guid>(options)
    {
        public DbSet<AppPermission> AppPermission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            _ = new AppPermissionConfiguration(modelBuilder.Entity<AppPermission>());

        }
    }
}