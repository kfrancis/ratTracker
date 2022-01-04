using Volo.Abp.Application.Dtos;

namespace RatTracker.Results
{
    public class ResultDto : FullAuditedEntityDto<Guid>
    {
        public DateTime TestDate { get; set; }
        public AgeBrackets Age { get; set; }
        public TestOutcome Outcome { get; set; }
        public Guid SchoolId { get; set; }
    }
}
