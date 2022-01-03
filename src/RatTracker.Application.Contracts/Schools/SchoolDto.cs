using System;
using Volo.Abp.Application.Dtos;

namespace RatTracker.Schools
{
    public class SchoolDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}