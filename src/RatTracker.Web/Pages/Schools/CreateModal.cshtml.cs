using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RatTracker.Schools;

namespace RatTracker.Web.Pages.Schools
{
    public class CreateModalModel : RatTrackerPageModel
    {
        [BindProperty]
        public SchoolCreateDto School { get; set; }

        private readonly ISchoolsAppService _schoolsAppService;

        public CreateModalModel(ISchoolsAppService schoolsAppService)
        {
            _schoolsAppService = schoolsAppService;
        }

        public async Task OnGetAsync()
        {
            School = new SchoolCreateDto();
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _schoolsAppService.CreateAsync(School).ConfigureAwait(false);
            return NoContent();
        }
    }
}