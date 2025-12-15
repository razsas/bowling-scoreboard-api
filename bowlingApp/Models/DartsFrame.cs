using bowlingApp.Constants;

namespace bowlingApp.Models
{
    public class DartsFrame : Frame
    {
        public int DartsCount => 1 + (Roll2.HasValue ? 1:0) + (Roll3.HasValue ? 1 : 0);
        public override string? ValidateInput()
        {
            if (IsInvalidScore(Roll1) || IsInvalidScore(Roll2) || IsInvalidScore(Roll3))
                return string.Format(DartsConstants.ValidationMessages.InvalidDartScore, DartsConstants.MinThrowScore, DartsConstants.MaxThrowScore);
            
            return null;
        }

        private static bool IsInvalidScore(int? dart)
        {
            return dart.HasValue && (dart.Value < DartsConstants.MinThrowScore || dart.Value > DartsConstants.MaxThrowScore);
        }
    }
}
