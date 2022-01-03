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
using RatTracker.Schools;

namespace RatTracker.Schools
{

    [Authorize(RatTrackerPermissions.Schools.Default)]
    public class SchoolsAppService : ApplicationService, ISchoolsAppService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolsAppService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public virtual async Task<PagedResultDto<SchoolDto>> GetListAsync(GetSchoolsInput input)
        {
            var totalCount = await _schoolRepository.GetCountAsync(input.FilterText, input.Name, input.Address1, input.Address2, input.Address3, input.City, input.PostalCode);
            var items = await _schoolRepository.GetListAsync(input.FilterText, input.Name, input.Address1, input.Address2, input.Address3, input.City, input.PostalCode, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SchoolDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<School>, List<SchoolDto>>(items)
            };
        }

        public virtual async Task<SchoolDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<School, SchoolDto>(await _schoolRepository.GetAsync(id));
        }

        [Authorize(RatTrackerPermissions.Schools.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _schoolRepository.DeleteAsync(id);
        }

        [Authorize(RatTrackerPermissions.Schools.Create)]
        public virtual async Task<SchoolDto> CreateAsync(SchoolCreateDto input)
        {

            var school = ObjectMapper.Map<SchoolCreateDto, School>(input);

            school = await _schoolRepository.InsertAsync(school, autoSave: true);
            return ObjectMapper.Map<School, SchoolDto>(school);
        }

        [Authorize(RatTrackerPermissions.Schools.Edit)]
        public virtual async Task<SchoolDto> UpdateAsync(Guid id, SchoolUpdateDto input)
        {

            var school = await _schoolRepository.GetAsync(id);
            ObjectMapper.Map(input, school);
            school = await _schoolRepository.UpdateAsync(school, autoSave: true);
            return ObjectMapper.Map<School, SchoolDto>(school);
        }
    }
}