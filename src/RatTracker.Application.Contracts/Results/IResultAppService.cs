using RatTracker.Common;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RatTracker.Results
{
    public interface IResultsAppService : IApplicationService
    {
        Task<PagedResultDto<ResultWithNavigationPropertiesDto>> GetListAsync(GetResultsInput input);

        Task<ResultWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ResultDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSchoolLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ResultDto> CreateAsync(ResultCreateDto input);

        Task<ResultDto> UpdateAsync(Guid id, ResultUpdateDto input);
    }
}
