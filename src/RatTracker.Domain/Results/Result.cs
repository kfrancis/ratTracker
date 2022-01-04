using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace RatTracker.Results
{
    public class Result : FullAuditedEntity<Guid>
    {
        public virtual DateTime TestDate { get; set; }

        [EnumDataType(typeof(AgeBrackets))]
        [UIHint("Enum")]
        public virtual AgeBrackets Age { get; set; }

        [EnumDataType(typeof(TestOutcome))]
        [UIHint("Enum")]
        public virtual TestOutcome Outcome { get; set; }

        public Guid SchoolId { get; set; }

        public Result()
        {
        }

        public Result(Guid id, DateTime testDate, AgeBrackets age, TestOutcome outcome, Guid schoolId)
        {
            Id = id;
            TestDate = testDate;
            Age = age;
            Outcome = outcome;
            SchoolId = schoolId;
        }
    }
}
