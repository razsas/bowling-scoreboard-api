using bowlingApp.Models;

namespace bowlingApp.Repository
{
    public interface IBowlingRepository
    {
        Task<Game> CreateNewGameAsync(string name);
        Task<Game?> GetGameAsync(int gameId);
        Task<Game?> AddFrameAsync(Frame frame, List<Frame> newFrames);
        Task<List<HighScore>> GetTopHighScoresAsync(int limit = 5);
        Task AddAndLimitHighScoresAsync(HighScore newScore, int limit = 5);
        Task<Game?> UpdateGameScoreAsync(Game game);
    }
}
