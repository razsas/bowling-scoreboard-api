using bowlingApp.Models;

namespace bowlingApp.Repository
{
    public interface IGameRepository<TGame, TFrame, THighScore>
        where TFrame : Frame
        where TGame : Game<TFrame>
        where THighScore : BaseHighScore
    {
        Task<TGame> CreateNewGameAsync(string name);
        Task<TGame?> GetGameAsync(int gameId);
        Task<TGame?> AddFrameAsync(TGame game, TFrame newFrame);
        Task<List<THighScore>> GetTopHighScoresAsync(int limit = 5);
        Task AddHighScoreAsync(THighScore newScore, int limit = 5);
        Task<TGame?> UpdateGameScoreAsync(TGame game);
    }
}
