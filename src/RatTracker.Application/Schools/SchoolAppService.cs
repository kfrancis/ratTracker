using Microsoft.AspNetCore.Authorization;
using RatTracker.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RatTracker.Schools
{
    [Authorize(RatTrackerPermissions.SchoolPermissions.Default)]
    public class SchoolsAppService : ApplicationService, ISchoolsAppService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolsAppService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public virtual async Task<PagedResultDto<SchoolDto>> GetListAsync(GetSchoolsInput input)
        {
            var totalCount = await _schoolRepository.GetCountAsync(input.FilterText, input.Name, input.Address1, input.Address2, input.Address3, input.City, input.PostalCode).ConfigureAwait(false);
            var items = await _schoolRepository.GetListAsync(input.FilterText, input.Name, input.Address1, input.Address2, input.Address3, input.City, input.PostalCode, input.Sorting, input.MaxResultCount, input.SkipCount).ConfigureAwait(false);

            return new PagedResultDto<SchoolDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<School>, List<SchoolDto>>(items)
            };
        }

        public virtual async Task<SchoolDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<School, SchoolDto>(await _schoolRepository.GetAsync(id).ConfigureAwait(false));
        }

        [Authorize(RatTrackerPermissions.SchoolPermissions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _schoolRepository.DeleteAsync(id).ConfigureAwait(false);
        }

        [Authorize(RatTrackerPermissions.SchoolPermissions.Create)]
        public virtual async Task<SchoolDto> CreateAsync(SchoolCreateDto input)
        {
            var school = ObjectMapper.Map<SchoolCreateDto, School>(input);

            school = await _schoolRepository.InsertAsync(school, autoSave: true).ConfigureAwait(false);
            return ObjectMapper.Map<School, SchoolDto>(school);
        }

        [Authorize(RatTrackerPermissions.SchoolPermissions.Edit)]
        public virtual async Task<SchoolDto> UpdateAsync(Guid id, SchoolUpdateDto input)
        {
            var school = await _schoolRepository.GetAsync(id).ConfigureAwait(false);
            ObjectMapper.Map(input, school);
            school = await _schoolRepository.UpdateAsync(school, autoSave: true).ConfigureAwait(false);
            return ObjectMapper.Map<School, SchoolDto>(school);
        }
    }
}
