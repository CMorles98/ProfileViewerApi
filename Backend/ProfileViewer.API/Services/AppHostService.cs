using ProfileViewer.Infrastructure.Context;
using ProfileViewer.Infrastructure.Helpers;

namespace ProfileViewer.Api.Services
{
    public class AppHostService(IServiceProvider serviceProvider) : IHostedService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProfileViewerContext>();
            if (context is not null) await InitialSeedHelper.SeedInitialData(context);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
