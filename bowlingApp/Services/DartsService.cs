using bowlingApp.Constants;
using bowlingApp.Models;
using bowlingApp.Models.Dto;
using bowlingApp.Repository;

namespace bowlingApp.Services
{
    public class DartsService(IGameRepository<DartsGame, DartsFrame, DartsHighScore> repository) : IGameService<DartsGame, DartsFrame, DartsHighScore>
    {
        private readonly IGameRepository<DartsGame, DartsFrame, DartsHighScore> _repository = repository;

        public Task<DartsGame?> GetGameAsync(int gameId)
        {
            return _repository.GetGameAsync(gameId);
        }

        public async Task<DartsGame> StartNewGameAsync(string name)
        {
            return await _repository.CreateNewGameAsync(name);
        }

        public async Task<List<DartsHighScore>> GetHighScoresAsync()
        {
            return await _repository.GetTopHighScoresAsync();
        }

        public async Task<TurnResult<DartsGame, DartsFrame>> AddFrameAsync(RollInput input)
        {
            var game = await _repository.GetGameAsync(input.GameId);

            if (game == null)
                return new TurnResult<DartsGame, DartsFrame>(false, string.Format(BowlingConstants.ValidationMessages.GameNotFound, input.GameId));

            if (game.Score == 0)
                return new TurnResult<DartsGame, DartsFrame>(false, DartsConstants.ValidationMessages.GameAlreadyOver);

            var newFrame = new DartsFrame
            {
                GameId = input.GameId,
                FrameIndex = game.Frames.Count,
                Roll1 = input.Roll1 ?? 0,
                Roll2 = input.Roll2,
                Roll3 = input.Roll3
            };

            string? error = newFrame.ValidateInput();
            if (error != null) return new TurnResult<DartsGame, DartsFrame>(false, error);

            // Calculate Frame Score
            newFrame.Score = newFrame.Roll1 + (newFrame.Roll2 ?? 0) + (newFrame.Roll3 ?? 0);

            // Check for Bust
            int remainingScore = game.Score - newFrame.Score;

            if (remainingScore < 0)
            {
                // Bust! Frame score counts as 0 for score reduction purposes (technically turn is voided)
                // In many digital implementations, we record the throws but score effect is 0.
                // Or we can say "Bust" and not record frames? Better to record what happened.
                // Let's set Frame Score to 0 to reflect no reduction, but keep rolls?
                // Issue: If we set Score to 0, history looks like they threw 0.
                // But DartsGame.UpdateGameScoreAsync does 501 - Sum(Frames.Score).
                // So if we want no reduction, this frame's SCORE property must be 0.
                newFrame.Score = 0;
                // We might want to mark it as bust in UI, maybe add a property later.
            }
            else if (remainingScore < DartsConstants.MinFinishScore && remainingScore > 0)
            {
                // You cannot finish on 1 (need double to finish, min double is 2).
                // So this is also a bust.
                newFrame.Score = 0;
            }

            // Add frame
            game.Frames.Add(newFrame);
            game.DartsCounter += newFrame.DartsCount;
            game.Score -= newFrame.Score;
            var updatedGame = await _repository.AddFrameAsync(game, newFrame);

            if (updatedGame != null && updatedGame.Score == 0)
            {
                // Winner! Typically need to check if last dart was a double.
                updatedGame = await _repository.UpdateGameScoreAsync(updatedGame);
            }

            return new TurnResult<DartsGame, DartsFrame>(true, State: updatedGame);
        }
    }
}
