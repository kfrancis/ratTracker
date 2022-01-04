using Shouldly;
using Xunit;

namespace RatTracker.Pages
{
    public class Index_Tests : RatTrackerWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/").ConfigureAwait(false);
            response.ShouldNotBeNull();
        }
    }
}
