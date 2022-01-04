namespace RatTracker.Common
{
    public class LookupDto<TKey>
    {
        public LookupDto(TKey id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public TKey Id { get; set; }

        public string DisplayName { get; set; }
    }
}