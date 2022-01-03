namespace RatTracker.Results
{
    public static class ResultConsts
    {
        private const string DefaultSorting = "{0}TestDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Result." : string.Empty);
        }

    }
}