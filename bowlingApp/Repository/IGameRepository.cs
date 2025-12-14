using bowlingApp.Models;

namespace bowlingApp.Repository
{
    public interface IGameRepository<TGame, TFrame>
        where TFrame : Frame
        where TGame : Game<TFrame>
    {
        Task<TGame> CreateNewGameAsync(string name);
        Task<TGame?> GetGameAsync(int gameId);
        Task<TGame?> AddFrameAsync(BowlingGame game, BowlingFrame newFrame);
        Task<List<HighScore>> GetTopHighScoresAsync(int limit = 5);
        Task AddHighScoreAsync(HighScore newScore, int limit = 5);
        Task<TGame?> UpdateGameScoreAsync(TGame game);
    }
}
