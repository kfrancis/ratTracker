using Volo.Abp.Application.Dtos;

namespace RatTracker.Schools
{
    public class GetSchoolsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public GetSchoolsInput()
        {
            FilterText = string.Empty;
            Name = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
        }
    }
}
