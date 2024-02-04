using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProfileViewer.Application.Authorization;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Infrastructure.Context;

namespace ProfileViewer.Infrastructure.Helpers
{
    public static class InitialSeedHelper
    {
        public static async Task SeedInitialData(ProfileViewerContext context)
        {
            await SeedUsers(context);
            await SeedRoles(context);
            await SeedUserRoles(context);
            await SeedAppPermissions(context);
            await SeedRoleClaims(context);
            await SeedUserRoleClaims(context);
        }

        private static async Task SeedUsers(ProfileViewerContext context)
        {
            if (context.Users.Any()) return;

            var user = new User()
            {
                Id = Guid.Parse("e3aa6f16-7a6d-4b19-aed8-20f2c66d36c8"),
                Email = "cmorles@verkku.com",
                CreationDate = DateTime.Now
            };
            
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "123qwe");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRoles(ProfileViewerContext context)
        {
            if (context.Roles.Any()) return;

            var roles = new List<Role>() {
                new("OPERATOR", Guid.Parse("8869ef42-fe91-4519-9a08-04df8eb30489")) ,
                new("ADMIN", Guid.Parse("c5c7c3b2-b3c6-4b7a-9353-50854fcf0497"))
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }
        private static async Task SeedUserRoles(ProfileViewerContext context)
        {
            if (context.UserRoles.Any()) return;

            var userRoles = new List<IdentityUserRole<Guid>>() {
                new() {
                    RoleId = Guid.Parse("c5c7c3b2-b3c6-4b7a-9353-50854fcf0497"),
                    UserId = Guid.Parse("e3aa6f16-7a6d-4b19-aed8-20f2c66d36c8")
                }
            };

            await context.UserRoles.AddRangeAsync(userRoles);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAppPermissions(ProfileViewerContext context)
        {
            var permissions = typeof(AppPermissions).GetNestedTypes().SelectMany(t => t.GetFields()).ToList();
            var dbPermissions = await context.AppPermission.ToListAsync();

            foreach (var dbPermission in dbPermissions)
            {
                var exists = permissions.Any(p => p.GetValue(null)?.ToString() == dbPermission.Name);

                if (exists) continue;

                context.AppPermission.Remove(dbPermission);
            }

            foreach (var permission in permissions)
            {
                var constantValue = permission.GetValue(null)?.ToString();

                if (constantValue is null || await context.AppPermission.AnyAsync(c => c.Name == constantValue))
                    continue;

                await context.AppPermission.AddAsync(new(constantValue));
            }
            await context.SaveChangesAsync();
        }

        private static async Task SeedRoleClaims(ProfileViewerContext context)
        {
            Guid adminRoleId = Guid.Parse("c5c7c3b2-b3c6-4b7a-9353-50854fcf0497");
            var permissions = typeof(AppPermissions).GetNestedTypes().SelectMany(t => t.GetFields()).ToList();

            foreach (var permission in permissions)
            {
                var constantValue = permission.GetValue(null)?.ToString();

                if (constantValue is null || await context.RoleClaims.AnyAsync(c => c.ClaimValue == constantValue && c.RoleId == adminRoleId))
                    continue;

                var newRoleClaim = new IdentityRoleClaim<Guid>
                {
                    RoleId = adminRoleId,
                    ClaimType = "Permission",
                    ClaimValue = constantValue
                };

                await context.RoleClaims.AddAsync(newRoleClaim);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedUserRoleClaims(ProfileViewerContext context)
        {
            var userRoles = await context.UserRoles.ToListAsync();
            var roleClaims = await context.RoleClaims.ToListAsync();

            foreach (var userRole in userRoles)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userRole.UserId);
                var userClaims = await context.UserClaims.Where(x => x.UserId == user.Id).ToListAsync();

                if (userClaims.Any()) continue;

                var userRoleClaims = roleClaims.Where(rc => rc.RoleId == userRole.RoleId);

                foreach (var userRoleClaim in userRoleClaims)
                {
                    var newUserRoleClaim = new IdentityUserClaim<Guid>
                    {
                        UserId = user!.Id,
                        ClaimType = userRoleClaim.ClaimType,
                        ClaimValue = userRoleClaim.ClaimValue
                    };

                    await context.UserClaims.AddAsync(newUserRoleClaim);
                }
            }

            await context.SaveChangesAsync();

        }



    }
}

