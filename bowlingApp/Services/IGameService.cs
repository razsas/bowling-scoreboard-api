using bowlingApp.Models;

namespace bowlingApp.Services
{
    public interface IGameService<TGame, TFrame>
        where TFrame : Frame
        where TGame : Game<TFrame>
    {
        Task<TGame> StartNewGameAsync(string name);
        Task<TurnResult<TGame, TFrame>> AddFrameAsync(RollInput input);
        Task<TGame?> GetGameAsync(int gameId);
        Task<List<HighScore>> GetHighScoresAsync();
    }

}
