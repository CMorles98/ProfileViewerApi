using ProfileViewer.Api.Services;

namespace ProfileViewer.Api.Setup
{
    public static class SetupHostServers
    {
        public static void AddHostServers(this IServiceCollection services) =>
            services.AddHostedService<AppHostService>();
    }
}
