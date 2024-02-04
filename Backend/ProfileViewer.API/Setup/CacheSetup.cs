namespace ProfileViewer.Api.Setup
{
    public static class CacheSetup
    {
        public static IServiceCollection AddCacheConfiguration(this IServiceCollection services) =>
            services.AddResponseCaching();
    }
}
