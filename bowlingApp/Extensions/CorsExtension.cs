using bowlingApp.Models;

namespace bowlingApp.Extensions
{
    public static class CorsExtension
    {
        public static IHostApplicationBuilder AddCustomCors(this IHostApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();

            if (settings != null)
            {
                builder.Services.AddCors(options =>
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
            }

            return builder;
        }
    }
}
