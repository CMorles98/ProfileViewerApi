using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ProfileViewer.Application.Localization;
using ProfileViewer.Application.Logger;
using ProfileViewer.Application.Services.Base;
using ProfileViewer.Application.Validators.Base;
using ProfileViewer.Domain.Localization;
using ProfileViewer.Domain.Logger;
using ProfileViewer.Domain.Services.Base;
using ProfileViewer.Domain.Validators.Base;

namespace ProfileViewer.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupApplicationLayer(this IServiceCollection services, ILogger logger)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddAutoMapper(assembly);

            services.AddSingleton<ILocalizationManager, LocalizationManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IValidatorManager, ValidatorManager>();
            services.AddScoped<ILoggerManager>(provider => new LoggerManager(logger));

            return services;
        }
    }
}