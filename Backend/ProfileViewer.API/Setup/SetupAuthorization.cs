using ProfileViewer.Application.Authorization;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Domain.Services.Base;

namespace ProfileViewer.Api.Setup
{
    public static class SetupAuthorization
    {
        public static void SetupAppAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var permissions = typeof(AppPermissions).GetNestedTypes().SelectMany(t => t.GetFields()).ToList();

                foreach (var permission in permissions)
                {
                    var claimValue = permission.GetValue(null)?.ToString();

                    if (claimValue is null) continue;

                    options.AddPolicy(claimValue, policy => policy.RequireAssertion(context =>
                        {
                            var userId = context.User.Claims.FirstOrDefault()?.Value;

                            if (string.IsNullOrEmpty(userId)) return false;

                            var repositoryManager = services.BuildServiceProvider().GetRequiredService<IRepositoryManager>();

                            var userClaims = Task.Run(async () => await repositoryManager.UserClaimsRepository.GetAll(x => x.UserId.Equals(Guid.Parse(userId))))
                            .GetAwaiter()
                            .GetResult();

                            if (userClaims.Any(x => (x.ClaimValue ?? string.Empty).Equals(claimValue))) 
                                return true;

                            return false;
                        }
                    ));
                }
            });
        }


    }
}
