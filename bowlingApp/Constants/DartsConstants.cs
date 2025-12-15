namespace bowlingApp.Constants
{
    public static class DartsConstants
    {
        public const int StartingScore = 501;
        public const int MaxThrowScore = 60;
        public const int MinThrowScore = 0;
        public const int MinFinishScore = 2;

        public static class ValidationMessages
        {
            public const string InvalidDartScore = "Invalid dart score. Score for a single dart is between {0} and {1} (Triple 20).";
            public const string GameAlreadyOver = "Game is already over.";
        }
    }
}
