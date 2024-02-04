using Asp.Versioning;

namespace ProfileViewer.Api.Setup
{
    public static class ApiVersioningSetup
    {
        public static void AddApiVersioningConfiguration(this IServiceCollection services) =>
            services.AddApiVersioning( 
                options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1,0);
            });
    }
}
