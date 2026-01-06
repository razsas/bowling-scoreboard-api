using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace bowlingApp.Extensions
{
    public static class OpenApiConfiguration
    {
        public static IServiceCollection AddAppOpenApi(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    const string schemeId = "Bearer";
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                    var scheme = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Enter your JWT token"
                    };

                    document.Components.SecuritySchemes[schemeId] = scheme;

                    var schemeReference = new OpenApiSecuritySchemeReference(schemeId, document);
                    var requirement = new OpenApiSecurityRequirement
                    {
                        [schemeReference] = []
                    };
                    document.Security = [requirement];

                    return Task.CompletedTask;
                });
            });

            return services;
        }

        public static void UseAppOpenApi(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("Bowling API .NET 10")
                           .WithTheme(ScalarTheme.Moon);

                    options.Authentication = new ScalarAuthenticationOptions
                    {
                        PreferredSecurityScheme = "Bearer"
                    };
                });
            }
        }
    }
}
