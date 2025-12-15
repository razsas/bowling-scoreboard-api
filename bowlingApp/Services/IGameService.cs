using bowlingApp.Models;
using bowlingApp.Models.Dto;

namespace bowlingApp.Services
{
    public interface IGameService<TGame, TFrame, THighScore>
        where TFrame : Frame
        where TGame : Game<TFrame>
        where THighScore : BaseHighScore
    {
        Task<TGame> StartNewGameAsync(string name);
        Task<TurnResult<TGame, TFrame>> AddFrameAsync(RollInput input);
        Task<TGame?> GetGameAsync(int gameId);
        Task<List<THighScore>> GetHighScoresAsync();
    }

}
