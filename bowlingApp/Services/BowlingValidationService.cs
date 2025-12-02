using bowlingApp.Constants;
using bowlingApp.Models;

namespace bowlingApp.Services
{
    public class BowlingValidationService : IBowlingValidationService
    {
        public string? ValidateRollInput(RollInput input, int currentFrameIndex)
        {
            var error = ValidatePinInput(input);
            if (error != null) return error;

            if (currentFrameIndex < BowlingConstants.LastFrameIndex)
                return ValidateStandardFrame(input);

            return ValidateTenthFrame(input);
        }

        private static string? ValidatePinInput(RollInput input)
        {
            if (!input.Roll1.HasValue)
                return BowlingConstants.ValidationMessages.Roll1Required;

            if (IsInvalidPinCount(input.Roll1) || IsInvalidPinCount(input.Roll2) || IsInvalidPinCount(input.Roll3))
                return BowlingConstants.ValidationMessages.InvalidPinCount;

            return null;
        }

        private static bool IsInvalidPinCount(int? roll)
        {
            return roll.HasValue && (roll.Value < BowlingConstants.MinPins || roll.Value > BowlingConstants.MaxPins);
        }

        private static string? ValidateStandardFrame(RollInput input)
        {
            if (input.Roll3.HasValue)
                return BowlingConstants.ValidationMessages.ThirdRollOnlyInTenth;

            if (input.IsStrike && input.Roll2.HasValue)
                return BowlingConstants.ValidationMessages.StrikeNoRoll2;

            if (!input.IsStrike)
            {
                if (!input.Roll2.HasValue)
                    return BowlingConstants.ValidationMessages.StandardFrameRequiresTwoRolls;

                if (input.Roll1 + input.Roll2.Value > BowlingConstants.MaxPins)
                    return string.Format(BowlingConstants.ValidationMessages.ExceedsTenPins,
                        input.Roll1 + input.Roll2.Value);
            }

            return null;
        }

        private static string? ValidateTenthFrame(RollInput input)
        {
            if (input.IsSpecialRoll)
            {
                if (!input.Roll3.HasValue)
                    return BowlingConstants.ValidationMessages.TenthFrameBonusRequired;
            }
            else
            {
                if (input.Roll3.HasValue)
                    return BowlingConstants.ValidationMessages.ThirdRollRequiresBonus;

                if (input.Roll2.HasValue && input.Roll1 + input.Roll2.Value > BowlingConstants.MaxPins)
                    return BowlingConstants.ValidationMessages.TenthFrameExceedsTenPins;
            }

            return null;
        }
    }
}

