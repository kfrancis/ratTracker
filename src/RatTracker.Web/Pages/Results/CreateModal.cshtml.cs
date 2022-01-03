using RatTracker.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RatTracker.Results;

namespace RatTracker.Web.Pages.Results
{
    public class CreateModalModel : RatTrackerPageModel
    {
        [BindProperty]
        public ResultCreateDto Result { get; set; }

        private readonly IResultsAppService _resultsAppService;

        public CreateModalModel(IResultsAppService resultsAppService)
        {
            _resultsAppService = resultsAppService;
        }

        public async Task OnGetAsync()
        {
            Result = new ResultCreateDto();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _resultsAppService.CreateAsync(Result);
            return NoContent();
        }
    }
}