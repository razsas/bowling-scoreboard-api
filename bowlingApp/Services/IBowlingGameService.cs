using bowlingApp.Models;

namespace bowlingApp.Services
{
    public interface IBowlingGameService
    {
        Task<Game> StartNewGameAsync(string name);
        Task<TurnResult> AddFrameAsync(RollInput input);
        Task<Game?> GetGameAsync(int gameId);
        Task<List<HighScore>> GetHighScoresAsync();
    }

}
