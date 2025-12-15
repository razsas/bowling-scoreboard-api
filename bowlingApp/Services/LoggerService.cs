using bowlingApp.Data;
using bowlingApp.Models;
using bowlingApp.Models.Dto;

namespace bowlingApp.Services
{
    public class LoggerService(ApplicationDbContext db)
    {
        readonly ApplicationDbContext _db = db;

        public async Task LogAsync(string level, string message, int gameId, RollInput input, int? frameId = null, string? stacktrace = null)
        {
            var log = new Log
            {
                Timestamp = DateTime.UtcNow,
                LogLevel = level,
                Message = message,
                StackTrace = stacktrace,
                GameId = gameId,
                FrameId = frameId,
                InputData = System.Text.Json.JsonSerializer.Serialize(input)
            };

            _db.AppLogs.Add(log);
            await _db.SaveChangesAsync();
        }

        public async Task LogErrorAsync(Exception ex, RollInput input, int gameId)
        {
            await LogAsync("Error", ex.Message, gameId, input, null, ex.StackTrace?? "");
        }
    }
}
