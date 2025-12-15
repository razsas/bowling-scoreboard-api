namespace bowlingApp.Constants
{
    public static class BowlingConstants
    {
        public const int MaxPins = 10;
        public const int MinPins = 0;
        public const int MaxFrames = 10;
        public const int LastFrameIndex = 9;
        public const int DefaultHighScoreLimit = 5;
        public const int UnsetRoll = -1;
        
        public static class ValidationMessages
        {
            public const string GameNotFound = "Game ID {0} not found.";
            public const string GameAlreadyOver = "The game is already over.";
            public const string Roll1Required = "Roll1 is required to start a frame.";
            public const string InvalidPinCount = "Each roll must knock down between 0 and 10 pins (0-10).";
            public const string ThirdRollOnlyInTenth = "Only the final frame (10th) can have a third roll.";
            public const string StrikeNoRoll2 = "A strike (Roll1=10) constitutes the entire frame. Roll2 must be null.";
            public const string StandardFrameRequiresTwoRolls = "A standard open or spare frame requires two rolls (Roll2 cannot be null).";
            public const string ExceedsTenPins = "Roll1 and Roll2 combined cannot exceed 10 pins ({0} > 10).";
            public const string TenthFrameBonusRequired = "A strike or spare in the 10th frame requires a third roll (bonus roll).";
            public const string ThirdRollRequiresBonus = "A third roll is only allowed in the 10th frame if the first two rolls resulted in a strike or spare.";
            public const string TenthFrameExceedsTenPins = "The first two rolls in the 10th frame cannot exceed 10 pins if no bonus roll is earned.";
        }
    }
}

