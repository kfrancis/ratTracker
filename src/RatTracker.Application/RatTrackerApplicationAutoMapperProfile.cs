using AutoMapper;
using RatTracker.Common;
using RatTracker.Results;
using RatTracker.Schools;
using Volo.Abp.AutoMapper;

namespace RatTracker
{
    public class RatTrackerApplicationAutoMapperProfile : Profile
    {
        public RatTrackerApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<SchoolCreateDto, School>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<SchoolUpdateDto, School>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<School, SchoolDto>()
                .ForMember(dest => dest.IsGeocoded, opt => opt.MapFrom(src => src.Location != null));

            CreateMap<ResultCreateDto, Result>().IgnoreFullAuditedObjectProperties().Ignore(x => x.Id);
            CreateMap<ResultUpdateDto, Result>().IgnoreFullAuditedObjectProperties().Ignore(x => x.Id);
            CreateMap<Result, ResultDto>();
            CreateMap<ResultWithNavigationProperties, ResultWithNavigationPropertiesDto>();
            CreateMap<School, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
