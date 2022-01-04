using RatTracker.Common;
using RatTracker.Schools;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using RatTracker.Results;

namespace RatTracker.Web.Pages.Results
{
    public class EditModalModel : RatTrackerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ResultUpdateDto Result { get; set; }

        public SchoolDto School { get; set; }

        private readonly IResultsAppService _resultsAppService;

        public EditModalModel(IResultsAppService resultsAppService)
        {
            _resultsAppService = resultsAppService;
        }

        public async Task OnGetAsync()
        {
            var resultWithNavigationPropertiesDto = await _resultsAppService.GetWithNavigationPropertiesAsync(Id).ConfigureAwait(false);
            Result = ObjectMapper.Map<ResultDto, ResultUpdateDto>(resultWithNavigationPropertiesDto.Result);

            School = resultWithNavigationPropertiesDto.School;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _resultsAppService.UpdateAsync(Id, Result).ConfigureAwait(false);
            return NoContent();
        }
    }
}