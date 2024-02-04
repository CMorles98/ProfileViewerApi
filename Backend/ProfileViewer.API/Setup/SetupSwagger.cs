using Microsoft.OpenApi.Models;

namespace ProfileViewer.Api.Setup
{
    public static class SetupSwagger
    {
        public static void SetupSwaggerConfiguration(this IServiceCollection services) =>
            services.AddSwaggerGen(c => 
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = " ProfileViewerApi", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Example: \"Bearer {token}\"",
                    });

                    var securityRequirement = new OpenApiSecurityRequirement {
                    { 
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }

                        }, new List<string>() }};

                    c.AddSecurityRequirement(securityRequirement);
                });
    }
}
