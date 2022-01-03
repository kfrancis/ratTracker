using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RatTracker.EntityFrameworkCore;
using RatTracker.Results;

namespace RatTracker.Web.Pages.Results
{
    public class CreateModel : RatTrackerPageModel
    {
        [BindProperty]
        public ResultCreateDto Result { get; set; }

        private readonly IResultsAppService _resultsAppService;

        public CreateModel(IResultsAppService resultsAppService)
        {
            _resultsAppService = resultsAppService;
        }

        public IActionResult OnGet()
        {
            Result = new ResultCreateDto();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _resultsAppService.CreateAsync(Result);
            return NoContent();
        }
    }
}
