using RatTracker.Shared;
using RatTracker.Schools;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using RatTracker.Permissions;
using RatTracker.Results;

namespace RatTracker.Results
{

    [Authorize(RatTrackerPermissions.Results.Default)]
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
            var totalCount = await _resultRepository.GetCountAsync(input.FilterText, input.TestDateMin, input.TestDateMax, input.Age, input.Outcome, input.SchoolId);
            var items = await _resultRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.TestDateMin, input.TestDateMax, input.Age, input.Outcome, input.SchoolId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ResultWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ResultWithNavigationProperties>, List<ResultWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ResultWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ResultWithNavigationProperties, ResultWithNavigationPropertiesDto>
                (await _resultRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ResultDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Result, ResultDto>(await _resultRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSchoolLookupAsync(LookupRequestDto input)
        {
            var query = (await _schoolRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<School>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<School>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(RatTrackerPermissions.Results.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _resultRepository.DeleteAsync(id);
        }

        [Authorize(RatTrackerPermissions.Results.Create)]
        public virtual async Task<ResultDto> CreateAsync(ResultCreateDto input)
        {
            if (input.SchoolId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["School"]]);
            }

            var result = ObjectMapper.Map<ResultCreateDto, Result>(input);

            result = await _resultRepository.InsertAsync(result, autoSave: true);
            return ObjectMapper.Map<Result, ResultDto>(result);
        }

        [Authorize(RatTrackerPermissions.Results.Edit)]
        public virtual async Task<ResultDto> UpdateAsync(Guid id, ResultUpdateDto input)
        {
            if (input.SchoolId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["School"]]);
            }

            var result = await _resultRepository.GetAsync(id);
            ObjectMapper.Map(input, result);
            result = await _resultRepository.UpdateAsync(result, autoSave: true);
            return ObjectMapper.Map<Result, ResultDto>(result);
        }
    }
}