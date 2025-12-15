
using bowlingApp.Data;
using bowlingApp.Models;
using bowlingApp.Repository;
using bowlingApp.Services;
using Microsoft.EntityFrameworkCore;

namespace bowlingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddScoped<IGameRepository<BowlingGame, BowlingFrame, BowlingHighScore>, BowlingRepository>();
            builder.Services.AddScoped<IGameService<BowlingGame, BowlingFrame, BowlingHighScore>, BowlingGameService>();
            builder.Services.AddScoped<IGameRepository<DartsGame, DartsFrame, DartsHighScore>, DartsRepository>();
            builder.Services.AddScoped<IGameService<DartsGame, DartsFrame, DartsHighScore>, DartsService>();
            builder.Services.AddScoped<LoggerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
