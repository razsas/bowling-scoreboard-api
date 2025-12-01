using bowlingApp.Models;
using bowlingApp.Repository;

namespace bowlingApp.Services
{
    public class BowlingGameService(IBowlingRepository repository) : IBowlingGameService
    {
        private readonly IBowlingRepository _repository = repository;
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
                return new TurnResult(false, $"Game ID {input.GameId} not found.");

            if (game.IsGameOver)
                return new TurnResult(false, "The game is already over.");

            string? error = BasicValidations(game.CurrentFrameNumber, input);
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
            var updatedFrames = CalculateAndUpdateScores([.. game.Frames, .. new[] { newFrame }], game.CurrentFrameNumber);
            updatedFrames.RemoveAt(updatedFrames.Count - 1);
            game = await _repository.AddFrameAsync(newFrame, updatedFrames);
            if (game.CurrentFrameNumber == 10)
            {
                game = await _repository.UpdateGameScoreAsync(game);
            }
            return new TurnResult(true, State: game);
        }
        public async Task<List<HighScore>> GetHighScoresAsync()
        {
            return await _repository.GetTopHighScoresAsync();
        }
        private static List<Frame> CalculateAndUpdateScores(List<Frame> frames, int currentFrameIndex)
        {
            var last2Frame = currentFrameIndex >= 2 ? frames[currentFrameIndex - 2] : null;
            var lastFrame = currentFrameIndex >= 1 ? frames[currentFrameIndex - 1] : null;
            var currentFrame = frames[currentFrameIndex];
            if (last2Frame != null && last2Frame.IsStrike && lastFrame.IsStrike)
                last2Frame.Score = 10 + 10 + currentFrame.Roll1;
            
            if (lastFrame != null && lastFrame.IsStrike)
                lastFrame.Score = 10 + currentFrame.Roll1 + (currentFrame.Roll2 ?? 0);
      
            if (lastFrame != null && lastFrame.IsSpare)
                lastFrame.Score = 10 + currentFrame.Roll1;
        
            return frames;
        }
        private static string? BasicValidations(int frame, RollInput input)
        {
            string? error;
            error = ValidatePinInput(input);
            if (error != null) return error;
            if (frame < 9) error = ValidateStandardFrame(input);
            else error = ValidateTenthFrame(input);
            return error;
        } 
        private static string? ValidateStandardFrame(RollInput input)
        {
            if (input.Roll3.HasValue)
                return "Only the final frame (10th) can have a third roll.";

            if (input.IsStrike && input.Roll2.HasValue)
                return "A strike (Roll1=10) constitutes the entire frame. Roll2 must be null.";

            if (!input.IsStrike)
            {
                if (!input.Roll2.HasValue)
                    return "A standard open or spare frame requires two rolls (Roll2 cannot be null).";

                if (input.Roll1 + input.Roll2.Value > 10)
                    return $"Roll1 and Roll2 combined cannot exceed 10 pins ({input.Roll1 + input.Roll2.Value} > 10).";
            }
            return null;
        }
        private static string? ValidateTenthFrame(RollInput input)
        {
            if (input.IsSpecialRoll)
            {
                if (!input.Roll3.HasValue)
                    return "A strike or spare in the 10th frame requires a third roll (bonus roll).";
            }
            else
            {
                if (input.Roll3.HasValue)
                    return "A third roll is only allowed in the 10th frame if the first two rolls resulted in a strike or spare.";

                if (input.Roll2.HasValue && input.Roll1 + input.Roll2.Value > 10)
                    return $"The first two rolls in the 10th frame cannot exceed 10 pins if no bonus roll is earned.";
            }

            return null;
        }
        private static string? ValidatePinInput(RollInput input)
        {
            if (!input.Roll1.HasValue) return "Roll1 is required to start a frame.";

            static bool IsValidPinCount(int? roll) => roll.HasValue && (roll.Value <= 0 && roll.Value >= 10);
            if (IsValidPinCount(input.Roll1) && IsValidPinCount(input.Roll2) && IsValidPinCount(input.Roll3))
                return "Each roll must knock down between 0 and 10 pins (0-10).";

            return null;
        }

    }

}
