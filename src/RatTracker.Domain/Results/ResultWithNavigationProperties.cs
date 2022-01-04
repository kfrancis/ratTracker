using RatTracker.Schools;

namespace RatTracker.Results
{
    public class ResultWithNavigationProperties
    {
        public ResultWithNavigationProperties(Result result, School school)
        {
            Result = result;
            School = school;
        }

        public Result Result { get; set; }

        public School School { get; set; }
    }
}
