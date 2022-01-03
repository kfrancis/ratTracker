using RatTracker.Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace RatTracker.Results
{
    public interface IResultRepository : IRepository<Result, Guid>
    {
        Task<ResultWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ResultWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? testDateMin = null,
            DateTime? testDateMax = null,
            AgeBrackets? age = null,
            TestOutcome? outcome = null,
            Guid? schoolId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Result>> GetListAsync(
                    string filterText = null,
                    DateTime? testDateMin = null,
                    DateTime? testDateMax = null,
                    AgeBrackets? age = null,
                    TestOutcome? outcome = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            DateTime? testDateMin = null,
            DateTime? testDateMax = null,
            AgeBrackets? age = null,
            TestOutcome? outcome = null,
            Guid? schoolId = null,
            CancellationToken cancellationToken = default);
    }
}