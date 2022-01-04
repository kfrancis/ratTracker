using AutoMapper;
using RatTracker.Results;
using RatTracker.Schools;

namespace RatTracker.Web
{
    public class RatTrackerWebAutoMapperProfile : Profile
    {
        public RatTrackerWebAutoMapperProfile()
        {
            CreateMap<SchoolDto, SchoolUpdateDto>();
            CreateMap<ResultDto, ResultUpdateDto>();
        }
    }
}
