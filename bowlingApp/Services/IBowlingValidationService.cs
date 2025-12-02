using bowlingApp.Models;

namespace bowlingApp.Services
{
    public interface IBowlingValidationService
    {
        string? ValidateRollInput(RollInput input, int currentFrameIndex);
    }
}

