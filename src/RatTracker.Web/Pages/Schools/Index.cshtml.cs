using RatTracker.Schools;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace RatTracker.Web.Pages.Schools
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; } = string.Empty;
        public string Address1Filter { get; set; } = string.Empty;
        public string Address2Filter { get; set; } = string.Empty;
        public string Address3Filter { get; set; } = string.Empty;
        public string CityFilter { get; set; } = string.Empty;
        public string PostalCodeFilter { get; set; } = string.Empty;

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
