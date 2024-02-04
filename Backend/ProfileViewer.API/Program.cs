using ProfileViewer.Api.Setup;
using ProfileViewer.Infrastructure;
using ProfileViewer.Application;
using Serilog;

namespace ProfileViewer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddCorsConfiguration();
            builder.Services.AddHostServers();
            //builder.Services.AddCacheConfiguration();
            builder.Services.AddApiVersioningConfiguration();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.SetupInfrastructureLayer(builder.Configuration)
                            .SetupApplicationLayer(Log.Logger);
            builder.Services.SetupSwaggerConfiguration();

            builder.Services.SetupAppAuthentication(builder.Configuration);
            builder.Services.SetupAppAuthorization();

            builder.Services.AddControllers(config =>
            {
                //config.CacheProfiles.Add("120SecondsDuration", new() { Duration = 120 });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProfileViewerApi V1");
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(Log.Logger);
            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
