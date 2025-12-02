using bowlingApp.Models;

namespace bowlingApp.Repository
{
    public interface IBowlingRepository
    {
        Task<Game> CreateNewGameAsync(string name);
        Task<Game?> GetGameAsync(int gameId);
        Task<Game?> AddFrameAsync(Game game, Frame newFrame);
        Task<List<HighScore>> GetTopHighScoresAsync(int limit = 5);
        Task AddHighScoreAsync(HighScore newScore, int limit = 5);
        Task<Game?> UpdateGameScoreAsync(Game game);
    }
}
