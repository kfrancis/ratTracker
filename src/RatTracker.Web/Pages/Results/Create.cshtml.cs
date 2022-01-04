using Microsoft.AspNetCore.Mvc;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v3;
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
            var verifyRequest = new reCAPTCHASiteVerifyRequest
            {
                Response = Result.Token
            };
            if (HttpContext.Connection.RemoteIpAddress != null)
            {
                verifyRequest.RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString();
            }
            var response = await _siteVerify.Verify(verifyRequest).ConfigureAwait(false);

            if (response.Success)
            {
                await _resultsAppService.CreateAsync(Result).ConfigureAwait(false);
            }

            return NoContent();
        }
    }
}
