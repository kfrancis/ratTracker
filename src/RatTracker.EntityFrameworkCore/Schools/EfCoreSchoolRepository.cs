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

namespace RatTracker.Schools
{
    public class EfCoreSchoolRepository : EfCoreRepository<RatTrackerDbContext, School, Guid>, ISchoolRepository
    {
        public EfCoreSchoolRepository(IDbContextProvider<RatTrackerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<School>> GetListAsync(
            string filterText = null,
            string name = null,
            string address1 = null,
            string address2 = null,
            string address3 = null,
            string city = null,
            string postalCode = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, address1, address2, address3, city, postalCode);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SchoolConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string address1 = null,
            string address2 = null,
            string address3 = null,
            string city = null,
            string postalCode = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, address1, address2, address3, city, postalCode);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<School> ApplyFilter(
            IQueryable<School> query,
            string filterText,
            string name = null,
            string address1 = null,
            string address2 = null,
            string address3 = null,
            string city = null,
            string postalCode = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.Address1.Contains(filterText) || e.Address2.Contains(filterText) || e.Address3.Contains(filterText) || e.City.Contains(filterText) || e.PostalCode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(address1), e => e.Address1.Contains(address1))
                    .WhereIf(!string.IsNullOrWhiteSpace(address2), e => e.Address2.Contains(address2))
                    .WhereIf(!string.IsNullOrWhiteSpace(address3), e => e.Address3.Contains(address3))
                    .WhereIf(!string.IsNullOrWhiteSpace(city), e => e.City.Contains(city))
                    .WhereIf(!string.IsNullOrWhiteSpace(postalCode), e => e.PostalCode.Contains(postalCode));
        }
    }
}