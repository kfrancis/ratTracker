using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using RatTracker.Common;
using RatTracker.Permissions;
using RatTracker.Schools;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace RatTracker.Results
{
    [Authorize(RatTrackerPermissions.ResultPermissionss.Default)]
    public class ResultsAppService : ApplicationService, IResultsAppService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IRepository<School, Guid> _schoolRepository;

        public ResultsAppService(IResultRepository resultRepository, IRepository<School, Guid> schoolRepository)
        {
            _resultRepository = resultRepository; _schoolRepository = schoolRepository;
        }

        public virtual async Task<PagedResultDto<ResultWithNavigationPropertiesDto>> GetListAsync(GetResultsInput input)
        {
            var totalCount = await _resultRepository.GetCountAsync(input.FilterText, input.TestDateMin, input.TestDateMax, input.Age, input.Outcome, input.SchoolId).ConfigureAwait(false);
            var items = await _resultRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.TestDateMin, input.TestDateMax, input.Age, input.Outcome, input.SchoolId, input.Sorting, input.MaxResultCount, input.SkipCount).ConfigureAwait(false);

            return new PagedResultDto<ResultWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ResultWithNavigationProperties>, List<ResultWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ResultWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ResultWithNavigationProperties, ResultWithNavigationPropertiesDto>
                (await _resultRepository.GetWithNavigationPropertiesAsync(id).ConfigureAwait(false));
        }

        public virtual async Task<ResultDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Result, ResultDto>(await _resultRepository.GetAsync(id).ConfigureAwait(false));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSchoolLookupAsync(LookupRequestDto input)
        {
            var query = (await _schoolRepository.GetQueryableAsync().ConfigureAwait(false))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<School>().ConfigureAwait(false);
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<School>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(RatTrackerPermissions.ResultPermissionss.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _resultRepository.DeleteAsync(id).ConfigureAwait(false);
        }

        [Authorize(RatTrackerPermissions.ResultPermissionss.Create)]
        public virtual async Task<ResultDto> CreateAsync(ResultCreateDto input)
        {
            if (input.SchoolId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["School"]]);
            }

            var result = ObjectMapper.Map<ResultCreateDto, Result>(input);

            result = await _resultRepository.InsertAsync(result, autoSave: true).ConfigureAwait(false);
            return ObjectMapper.Map<Result, ResultDto>(result);
        }

        [Authorize(RatTrackerPermissions.ResultPermissionss.Edit)]
        public virtual async Task<ResultDto> UpdateAsync(Guid id, ResultUpdateDto input)
        {
            if (input.SchoolId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["School"]]);
            }

            var result = await _resultRepository.GetAsync(id).ConfigureAwait(false);
            ObjectMapper.Map(input, result);
            result = await _resultRepository.UpdateAsync(result, autoSave: true).ConfigureAwait(false);
            return ObjectMapper.Map<Result, ResultDto>(result);
        }
    }
}
