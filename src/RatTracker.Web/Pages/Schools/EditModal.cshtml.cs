using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using RatTracker.Schools;

namespace RatTracker.Web.Pages.Schools
{
    public class EditModalModel : RatTrackerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public SchoolUpdateDto School { get; set; }

        private readonly ISchoolsAppService _schoolsAppService;

        public EditModalModel(ISchoolsAppService schoolsAppService)
        {
            _schoolsAppService = schoolsAppService;
        }

        public async Task OnGetAsync()
        {
            var school = await _schoolsAppService.GetAsync(Id).ConfigureAwait(false);
            School = ObjectMapper.Map<SchoolDto, SchoolUpdateDto>(school);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _schoolsAppService.UpdateAsync(Id, School).ConfigureAwait(false);
            return NoContent();
        }
    }
}