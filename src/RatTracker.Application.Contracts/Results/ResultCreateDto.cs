using RatTracker.Results;
using System;
using System.ComponentModel.DataAnnotations;

namespace RatTracker.Results
{
    public class ResultCreateDto
    {
        [Required]
        public DateTime TestDate { get; set; }
        [Required]
        public AgeBrackets Age { get; set; } = ((AgeBrackets[])Enum.GetValues(typeof(AgeBrackets)))[0];
        [Required]
        public TestOutcome Outcome { get; set; } = ((TestOutcome[])Enum.GetValues(typeof(TestOutcome)))[0];
        public Guid SchoolId { get; set; }
        [Required]
        public string Token { get; set; }
    }
}