using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RatTracker.Schools
{
    public interface ISchoolsAppService : IApplicationService
    {
        Task<PagedResultDto<SchoolDto>> GetListAsync(GetSchoolsInput input);

        Task<SchoolDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SchoolDto> CreateAsync(SchoolCreateDto input);

        Task<SchoolDto> UpdateAsync(Guid id, SchoolUpdateDto input);

        Task<IEnumerable<GeoCoordinate>> GetGeoCoordinateAsync(double lat, double lng);
    }
}
