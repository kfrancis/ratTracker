using RatTracker.Results;
using System;
using System.ComponentModel.DataAnnotations;

namespace RatTracker.Results
{
    public class ResultUpdateDto
    {
        [Required]
        public DateTime TestDate { get; set; }
        [Required]
        public AgeBrackets Age { get; set; }
        [Required]
        public TestOutcome Outcome { get; set; }
        public Guid SchoolId { get; set; }
    }
}