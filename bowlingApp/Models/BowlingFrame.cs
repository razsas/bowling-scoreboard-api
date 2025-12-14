using bowlingApp.Constants;

namespace bowlingApp.Models
{
    public class BowlingFrame : Frame
    {
        public BowlingFrame() { }
        public BowlingFrame(RollInput input, int frameIndex)
        {
            GameId = input.GameId;
            FrameIndex = frameIndex;
            Roll1 = input.Roll1 ?? 0;
            Roll2 = input.Roll2;
            Roll3 = input.Roll3;
            Score = Roll1 + (Roll2 ?? 0) + (Roll3 ?? 0);
        }
        public bool IsStrike => Roll1 == BowlingConstants.MaxPins;
        public bool IsSpare => !IsStrike && Roll2.HasValue && (Roll1 + Roll2.Value == BowlingConstants.MaxPins);
        public bool IsTenthFrame => FrameIndex == BowlingConstants.LastFrameIndex;
        public bool IsSpecialRoll => IsStrike || IsSpare;

        public override string? ValidateRollInput()
        {
            var error = ValidatePinInput();
            if (error != null) return error;

            if (!IsTenthFrame)
                return ValidateStandardFrame();

            return ValidateTenthFrame();
        }

        private string? ValidatePinInput()
        {
            if (Roll1 == 0)
                return BowlingConstants.ValidationMessages.Roll1Required;

            if (IsInvalidPinCount(Roll1) || IsInvalidPinCount(Roll2) || IsInvalidPinCount(Roll3))
                return BowlingConstants.ValidationMessages.InvalidPinCount;

            return null;
        }

        private static bool IsInvalidPinCount(int? roll)
        {
            return roll.HasValue && (roll.Value < BowlingConstants.MinPins || roll.Value > BowlingConstants.MaxPins);
        }

        private string? ValidateStandardFrame()
        {
            if (Roll3.HasValue)
                return BowlingConstants.ValidationMessages.ThirdRollOnlyInTenth;

            if (IsStrike && Roll2.HasValue)
                return BowlingConstants.ValidationMessages.StrikeNoRoll2;

            if (!IsStrike)
            {
                if (!Roll2.HasValue)
                    return BowlingConstants.ValidationMessages.StandardFrameRequiresTwoRolls;

                if (Roll1 + Roll2.Value > BowlingConstants.MaxPins)
                    return string.Format(BowlingConstants.ValidationMessages.ExceedsTenPins,
                        Roll1 + Roll2.Value);
            }

            return null;
        }

        private string? ValidateTenthFrame()
        {
            if (IsSpecialRoll)
            {
                if (!Roll3.HasValue)
                    return BowlingConstants.ValidationMessages.TenthFrameBonusRequired;
            }
            else
            {
                if (Roll3.HasValue)
                    return BowlingConstants.ValidationMessages.ThirdRollRequiresBonus;

                if (Roll2.HasValue && Roll1 + Roll2.Value > BowlingConstants.MaxPins)
                    return BowlingConstants.ValidationMessages.TenthFrameExceedsTenPins;
            }

            return null;
        }
    }
}
