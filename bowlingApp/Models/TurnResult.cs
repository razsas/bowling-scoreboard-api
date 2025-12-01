namespace bowlingApp.Models
{
    public record TurnResult(bool IsSuccess, string? ErrorMessage = null, Game? State = null);
}
