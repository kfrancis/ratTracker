using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using RatTracker.Schools;
using RatTracker.Common;

namespace RatTracker.Web.Pages.Schools
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public string Address1Filter { get; set; }
        public string Address2Filter { get; set; }
        public string Address3Filter { get; set; }
        public string CityFilter { get; set; }
        public string PostalCodeFilter { get; set; }

        private readonly ISchoolsAppService _schoolsAppService;

        public IndexModel(ISchoolsAppService schoolsAppService)
        {
            _schoolsAppService = schoolsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}