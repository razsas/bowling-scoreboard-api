using bowlingApp.Models;
using bowlingApp.Repository;
using bowlingApp.Constants;
using bowlingApp.Models.Dto;

namespace bowlingApp.Services
{
    public class BowlingGameService(IGameRepository<BowlingGame, BowlingFrame, BowlingHighScore> repository) : IGameService<BowlingGame, BowlingFrame, BowlingHighScore>
    {
        private readonly IGameRepository<BowlingGame, BowlingFrame, BowlingHighScore> _repository = repository;
        public Task<BowlingGame?> GetGameAsync(int gameId)
        {
            return _repository.GetGameAsync(gameId);
        }
        public async Task<BowlingGame> StartNewGameAsync(string name)
        {
            return await _repository.CreateNewGameAsync(name);
        }
        public async Task<List<BowlingHighScore>> GetHighScoresAsync()
        {
            return await _repository.GetTopHighScoresAsync();
        }
        public async Task<TurnResult<BowlingGame, BowlingFrame>> AddFrameAsync(RollInput input)
        {
            var game = await _repository.GetGameAsync(input.GameId);

            if (game == null)
                return new TurnResult<BowlingGame, BowlingFrame>(false, string.Format(BowlingConstants.ValidationMessages.GameNotFound, input.GameId));

            if (game.IsGameOver)
                return new TurnResult<BowlingGame, BowlingFrame>(false, BowlingConstants.ValidationMessages.GameAlreadyOver);

            var newFrame = new BowlingFrame(input, game.CurrentFrameNumber);

            string? error = newFrame.ValidateInput();
            if (error != null) return new TurnResult<BowlingGame, BowlingFrame>(false, error);

            game.UpdatePreviousFrameScores(newFrame);
            game.Frames.Add(newFrame);

            var updatedGame = await _repository.AddFrameAsync(game, newFrame);
            if (updatedGame != null && updatedGame.CurrentFrameNumber == BowlingConstants.MaxFrames)
            {
                updatedGame = await _repository.UpdateGameScoreAsync(updatedGame);
            }
            return new TurnResult<BowlingGame, BowlingFrame>(true, State: updatedGame);
        }
    }
}
