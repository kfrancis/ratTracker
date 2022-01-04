using Volo.Abp.Application.Dtos;

namespace RatTracker.Common
{
    public class LookupRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }

        public LookupRequestDto()
        {
            MaxResultCount = MaxMaxResultCount;
            Filter = string.Empty;
        }
    }
}