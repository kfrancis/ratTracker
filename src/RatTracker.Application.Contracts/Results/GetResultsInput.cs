using RatTracker.Results;
using Volo.Abp.Application.Dtos;
using System;

namespace RatTracker.Results
{
    public class GetResultsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public DateTime? TestDateMin { get; set; }
        public DateTime? TestDateMax { get; set; }
        public AgeBrackets? Age { get; set; }
        public TestOutcome? Outcome { get; set; }
        public Guid? SchoolId { get; set; }

        public GetResultsInput()
        {

        }
    }
}