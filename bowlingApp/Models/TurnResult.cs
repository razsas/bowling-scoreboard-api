namespace bowlingApp.Models
{
    public record TurnResult<TGame, TFrame>(bool IsSuccess, string? ErrorMessage = null, TGame? State = null) where TFrame : Frame where TGame : Game<TFrame>;
}
