using RatTracker.Schools;

using System;
using Volo.Abp.Application.Dtos;

namespace RatTracker.Results
{
    public class ResultWithNavigationPropertiesDto
    {
        public ResultDto Result { get; set; }

        public SchoolDto School { get; set; }

    }
}