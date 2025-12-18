using bowlingApp.Models;
using bowlingApp.Models.Dto;
using bowlingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace bowlingApp.Controllers
{
    [ApiController]
    public abstract class GameController<TGame, TFrame, THighScore>(
    IGameService<TGame, TFrame, THighScore> gameService,
    LoggerService loggerService) : ControllerBase
    where TFrame : Frame
    where TGame : Game<TFrame>
    where THighScore : BaseHighScore
    {
        protected readonly IGameService<TGame, TFrame, THighScore> _gameService = gameService;
        protected readonly LoggerService _logService = loggerService;

        [HttpPost("start")]
        public virtual async Task<IActionResult> StartNewGame([FromBody] StartGameRequest request)
        {
            var game = await _gameService.StartNewGameAsync(request.GameName);
            return CreatedAtAction(nameof(GetGame), new { gameId = game.Id }, game);
        }

        [HttpPost("turn")]
        public virtual async Task<IActionResult> PostTurn([FromBody] RollInput input)
        {
            var result = await _gameService.AddFrameAsync(input);

            if (!result.IsSuccess)
            {
                await _logService.LogAsync("Warning", result.ErrorMessage ?? "Error", input.GameId, input);
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{gameId}")]
        public virtual async Task<IActionResult> GetGame(int gameId)
        {
            var game = await _gameService.GetGameAsync(gameId);
            return game == null ? NotFound($"Game ID {gameId} not found.") : Ok(game);
        }

        [HttpGet("highscores")]
        public virtual async Task<IActionResult> GetHighScores()
        {
            var scores = await _gameService.GetHighScoresAsync();
            return Ok(scores);
        }
    }
}
