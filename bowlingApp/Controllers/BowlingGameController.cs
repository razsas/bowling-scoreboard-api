using bowlingApp.Models;
using bowlingApp.Models.Dto;
using bowlingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace bowlingApp.Controllers
{
    [ApiController]
    [Route("api/bowling")]
    public class BowlingGameController(IBowlingGameService gameService) : ControllerBase
    {
        private readonly IBowlingGameService _gameService = gameService;

        [HttpPost("start")]
        public async Task<IActionResult> StartNewGame([FromBody] StartGameRequest request)
        {
            var game = await _gameService.StartNewGameAsync(request.GameName);
            return CreatedAtAction(nameof(GetGame), new { gameId = game.Id }, game);
        }

        [HttpPost("turn")]
        public async Task<IActionResult> PostTurn([FromBody] RollInput input)
        {
            var result = await _gameService.AddFrameAsync(input);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(int gameId)
        {
            var game = await _gameService.GetGameAsync(gameId);
            if (game == null)
            {
                return NotFound($"Game ID {gameId} not found.");
            }
            return Ok(game);
        }

        [HttpGet("highscores")]
        public async Task<IActionResult> GetHighScores()
        {
            var scores = await _gameService.GetHighScoresAsync();
            return Ok(scores);
        }
    }
}
