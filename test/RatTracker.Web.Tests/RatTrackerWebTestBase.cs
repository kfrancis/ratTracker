using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Volo.Abp.AspNetCore.TestBase;

namespace RatTracker
{
    public abstract class RatTrackerWebTestBase : AbpAspNetCoreIntegratedTestBase<RatTrackerWebTestStartup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return base
                .CreateHostBuilder()
                .UseContentRoot(WebContentDirectoryFinder.CalculateContentRootFolder());
        }

        protected virtual async Task<T> GetResponseAsObjectAsync<T>(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var strResponse = await GetResponseAsStringAsync(url, expectedStatusCode).ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(strResponse, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

        protected virtual async Task<string> GetResponseAsStringAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await GetResponseAsync(url, expectedStatusCode).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        protected virtual async Task<HttpResponseMessage> GetResponseAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await Client.GetAsync(url).ConfigureAwait(false);
            response.StatusCode.ShouldBe(expectedStatusCode);
            return response;
        }
    }
}
