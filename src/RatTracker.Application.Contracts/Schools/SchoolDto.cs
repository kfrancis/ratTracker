using Volo.Abp.Application.Dtos;

namespace RatTracker.Schools
{
    public class SchoolDto : FullAuditedEntityDto<Guid>
    {
        public SchoolDto()
        {
            Name = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
        }

        public SchoolDto(string name, string address1, string address2, string address3, string city, string postalCode)
        {
            Name = name;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            City = city;
            PostalCode = postalCode;
        }

        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool IsGeocoded { get; set; }
    }
}
