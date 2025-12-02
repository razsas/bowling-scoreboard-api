using bowlingApp.Models;
using bowlingApp.Repository;
using bowlingApp.Constants;

namespace bowlingApp.Services
{
    public class BowlingGameService(IBowlingRepository repository, IBowlingValidationService validationService) : IBowlingGameService
    {
        private readonly IBowlingRepository _repository = repository;
        private readonly IBowlingValidationService _validationService = validationService;
        public Task<Game?> GetGameAsync(int gameId)
        {
            return _repository.GetGameAsync(gameId);
        }
        public async Task<Game> StartNewGameAsync(string name)
        {
            var newGame = await _repository.CreateNewGameAsync(name);
            return newGame;
        }
        public async Task<TurnResult> AddFrameAsync(RollInput input)
        {
            var game = await _repository.GetGameAsync(input.GameId);

            if (game == null)
                return new TurnResult(false, string.Format(BowlingConstants.ValidationMessages.GameNotFound, input.GameId));

            if (game.IsGameOver)
                return new TurnResult(false, BowlingConstants.ValidationMessages.GameAlreadyOver);

            string? error = _validationService.ValidateRollInput(input, game.CurrentFrameNumber);
            if (error != null) return new TurnResult(false, error);

            var newFrame = new Frame
            {
                GameId = input.GameId,
                FrameIndex = game.CurrentFrameNumber,
                Roll1 = input.Roll1 ?? 0,
                Roll2 = input.Roll2,
                Roll3 = input.Roll3,
                Score = (input.Roll1 ?? 0) + (input.Roll2 ?? 0) + (input.Roll3 ?? 0)
            };
            UpdatePreviousFrameScores(game.Frames, newFrame, game.CurrentFrameNumber);
            game.Frames.Add(newFrame);
            var updatedGame = await _repository.AddFrameAsync(game, newFrame);
            if (updatedGame != null && updatedGame.CurrentFrameNumber == BowlingConstants.MaxFrames)
            {
                updatedGame = await _repository.UpdateGameScoreAsync(updatedGame);
            }
            return new TurnResult(true, State: updatedGame);
        }
        public async Task<List<HighScore>> GetHighScoresAsync()
        {
            return await _repository.GetTopHighScoresAsync();
        }
        private static void UpdatePreviousFrameScores(List<Frame> frames, Frame newFrame, int currentFrameIndex)
        {
            if (newFrame.FrameIndex == 0) return;
            var lastFrame = frames.LastOrDefault(f => f.FrameIndex == currentFrameIndex - 1);
            var last2Frame = frames.LastOrDefault(f => f.FrameIndex == currentFrameIndex - 2);

            if (last2Frame != null && last2Frame.IsStrike && lastFrame.IsStrike)
            {
                last2Frame.Score = (BowlingConstants.MaxPins * 2) + newFrame.Roll1;
            }

            if (lastFrame != null)
            {
                if (lastFrame.IsStrike)
                {
                    lastFrame.Score = BowlingConstants.MaxPins + newFrame.Roll1 + (newFrame.Roll2 ?? 0);
                }

                if (lastFrame.IsSpare)
                {
                    lastFrame.Score = BowlingConstants.MaxPins + newFrame.Roll1;
                }
            }
        }
    }
}
