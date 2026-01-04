using bowlingApp.Models;

namespace bowlingApp.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CorsSettings>(configuration.GetSection("CorsSettings"));

            var settings = configuration.GetSection("CorsSettings").Get<CorsSettings>();

            if (settings == null) return services;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    if (settings.AllowedOrigins.Contains("*"))
                        policy.AllowAnyOrigin();
                    else
                        policy.WithOrigins([.. settings.AllowedOrigins]);

                    if (settings.AllowedMethods.Contains("*"))
                        policy.AllowAnyMethod();
                    else
                        policy.WithMethods([.. settings.AllowedMethods]);

                    if (settings.AllowedHeaders.Contains("*"))
                        policy.AllowAnyHeader();
                    else
                        policy.WithHeaders([.. settings.AllowedHeaders]);
                });
            });

            return services;
        }
    }
}
