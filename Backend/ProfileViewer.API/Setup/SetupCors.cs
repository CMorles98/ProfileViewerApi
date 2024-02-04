namespace ProfileViewer.Api.Setup
{
    public static class SetupCors
    {
        public static void AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options => 
                options.AddPolicy("ProfileViewerPolicy", builder => 
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()));

    }
}
