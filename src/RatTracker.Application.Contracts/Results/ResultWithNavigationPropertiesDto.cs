using RatTracker.Schools;

namespace RatTracker.Results
{
    public class ResultWithNavigationPropertiesDto
    {
        public ResultWithNavigationPropertiesDto(ResultDto result, SchoolDto school)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
            School = school ?? throw new ArgumentNullException(nameof(school));
        }

        public ResultDto Result { get; set; }

        public SchoolDto School { get; set; }
    }
}
