using Volo.Abp.Domain.Repositories;

namespace RatTracker.Schools
{
    public interface ISchoolRepository : IRepository<School, Guid>
    {
        Task<List<School>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? address1 = null,
            string? address2 = null,
            string? address3 = null,
            string? city = null,
            string? postalCode = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? address1 = null,
            string? address2 = null,
            string? address3 = null,
            string? city = null,
            string? postalCode = null,
            CancellationToken cancellationToken = default);
    }
}
