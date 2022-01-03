using RatTracker.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using RatTracker.EntityFrameworkCore;

namespace RatTracker.Results
{
    public class EfCoreResultRepository : EfCoreRepository<RatTrackerDbContext, Result, Guid>, IResultRepository
    {
        public EfCoreResultRepository(IDbContextProvider<RatTrackerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ResultWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryForNavigationPropertiesAsync())
                .FirstOrDefaultAsync(e => e.Result.Id == id, GetCancellationToken(cancellationToken));
        }

        public async Task<List<ResultWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? testDateMin = null,
            DateTime? testDateMax = null,
            AgeBrackets? age = null,
            TestOutcome? outcome = null,
            Guid? schoolId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, testDateMin, testDateMax, age, outcome, schoolId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ResultConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ResultWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from result in (await GetDbSetAsync())
                   join school in (await GetDbContextAsync()).Schools on result.SchoolId equals school.Id into schools
                   from school in schools.DefaultIfEmpty()

                   select new ResultWithNavigationProperties
                   {
                       Result = result,
                       School = school
                   };
        }

        protected virtual IQueryable<ResultWithNavigationProperties> ApplyFilter(
            IQueryable<ResultWithNavigationProperties> query,
            string filterText,
            DateTime? testDateMin = null,
            DateTime? testDateMax = null,
            AgeBrackets? age = null,
            TestOutcome? outcome = null,
            Guid? schoolId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(testDateMin.HasValue, e => e.Result.TestDate >= testDateMin.Value)
                    .WhereIf(testDateMax.HasValue, e => e.Result.TestDate <= testDateMax.Value)
                    .WhereIf(age.HasValue, e => e.Result.Age == age)
                    .WhereIf(outcome.HasValue, e => e.Result.Outcome == outcome)
                    .WhereIf(schoolId != null && schoolId != Guid.Empty, e => e.School != null && e.School.Id == schoolId);
        }

        public async Task<List<Result>> GetListAsync(
            string filterText = null,
            DateTime? testDateMin = null,
            DateTime? testDateMax = null,
            AgeBrackets? age = null,
            TestOutcome? outcome = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, testDateMin, testDateMax, age, outcome);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ResultConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? testDateMin = null,
            DateTime? testDateMax = null,
            AgeBrackets? age = null,
            TestOutcome? outcome = null,
            Guid? schoolId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, testDateMin, testDateMax, age, outcome, schoolId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Result> ApplyFilter(
            IQueryable<Result> query,
            string filterText,
            DateTime? testDateMin = null,
            DateTime? testDateMax = null,
            AgeBrackets? age = null,
            TestOutcome? outcome = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(testDateMin.HasValue, e => e.TestDate >= testDateMin.Value)
                    .WhereIf(testDateMax.HasValue, e => e.TestDate <= testDateMax.Value)
                    .WhereIf(age.HasValue, e => e.Age == age)
                    .WhereIf(outcome.HasValue, e => e.Outcome == outcome);
        }
    }
}