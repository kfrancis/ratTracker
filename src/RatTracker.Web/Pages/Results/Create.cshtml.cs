using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v3;
using RatTracker.EntityFrameworkCore;
using RatTracker.Results;

namespace RatTracker.Web.Pages.Results
{
    public class CreateModel : RatTrackerPageModel
    {
        [BindProperty]
        public ResultCreateDto Result { get; set; }

        private readonly IResultsAppService _resultsAppService;
        private readonly IreCAPTCHASiteVerifyV3 _siteVerify;

        public CreateModel(IResultsAppService resultsAppService, IreCAPTCHASiteVerifyV3 siteVerify)
        {
            _resultsAppService = resultsAppService;
            _siteVerify = siteVerify;
        }

        public IActionResult OnGet()
        {
            Result = new ResultCreateDto();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _siteVerify.Verify(new reCAPTCHASiteVerifyRequest
            {
                Response = Result.Token,
                RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
            });

            if (response.Success)
            {
                await _resultsAppService.CreateAsync(Result);
            }
            
            return NoContent();
        }
    }
}
