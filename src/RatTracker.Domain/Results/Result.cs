using RatTracker.Results;
using RatTracker.Schools;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace RatTracker.Results
{
    public class Result : FullAuditedEntity<Guid>
    {
        public virtual DateTime TestDate { get; set; }

        public virtual AgeBrackets Age { get; set; }

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