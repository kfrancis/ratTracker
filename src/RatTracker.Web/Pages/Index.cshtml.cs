using Microsoft.AspNetCore.Mvc;
using RatTracker.Schools;

namespace RatTracker.Web.Pages
{
    public class IndexModel : RatTrackerPageModel
    {
        private readonly ISchoolsAppService _schoolsAppService;

        public IndexModel(ISchoolsAppService schoolsAppService)
        {
            _schoolsAppService = schoolsAppService;
        }

        public void OnGet()
        {

        }
    }
}
