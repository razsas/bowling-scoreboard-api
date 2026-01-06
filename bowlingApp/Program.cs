
using bowlingApp.Data;
using bowlingApp.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace bowlingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddAppOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.AddCustomCors();

            var app = builder.Build();

            app.UseAppOpenApi();

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
