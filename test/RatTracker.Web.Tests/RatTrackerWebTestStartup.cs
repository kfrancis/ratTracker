using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RatTracker
{
    public class RatTrackerWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<RatTrackerWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}
