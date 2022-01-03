using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using RatTracker.Results;
using RatTracker.Shared;

namespace RatTracker.Web.Pages.Results
{
    public class IndexModel : AbpPageModel
    {
        public DateTime? TestDateFilterMin { get; set; }

        public DateTime? TestDateFilterMax { get; set; }
        public AgeBrackets? AgeFilter { get; set; }
        public TestOutcome? OutcomeFilter { get; set; }
        [SelectItems(nameof(SchoolLookupList))]
        public Guid SchoolIdFilter { get; set; }
        public List<SelectListItem> SchoolLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IResultsAppService _resultsAppService;

        public IndexModel(IResultsAppService resultsAppService)
        {
            _resultsAppService = resultsAppService;
        }

        public async Task OnGetAsync()
        {
            SchoolLookupList.AddRange((
                    await _resultsAppService.GetSchoolLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}