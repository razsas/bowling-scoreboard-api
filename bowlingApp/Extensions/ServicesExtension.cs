using bowlingApp.Models;
using bowlingApp.Repository;
using bowlingApp.Services;

namespace bowlingApp.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // --- Bowling Module ---
            services.AddScoped<IGameRepository<BowlingGame, BowlingFrame, BowlingHighScore>, BowlingRepository>();
            services.AddScoped<IGameService<BowlingGame, BowlingFrame, BowlingHighScore>, BowlingService>();

            // --- Darts Module ---
            services.AddScoped<IGameRepository<DartsGame, DartsFrame, DartsHighScore>, DartsRepository>();
            services.AddScoped<IGameService<DartsGame, DartsFrame, DartsHighScore>, DartsService>();

            // --- Shared Services ---
            services.AddScoped<LoggerService>();

            return services;
        }
    }
}
