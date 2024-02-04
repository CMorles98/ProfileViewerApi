using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileViewer.Domain.Authentication;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Infrastructure.Authentication;
using ProfileViewer.Infrastructure.Context;
using ProfileViewer.Infrastructure.Repositories;

namespace ProfileViewer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupInfrastructureLayer(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddDbContext<ProfileViewerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                d => {
                    d.EnableRetryOnFailure();
                    d.MigrationsAssembly(assembly.FullName); 
                }));

            services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ProfileViewerContext>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IJwtManager, JwtManager>();

            return services;
        }
    }
}