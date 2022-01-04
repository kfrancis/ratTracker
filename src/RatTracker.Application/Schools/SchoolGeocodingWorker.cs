using System.Diagnostics;
using Geocoding;
using Geocoding.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace RatTracker.Schools
{
    public class SchoolGeocodingWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public SchoolGeocodingWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
        {
            Timer.Period = 600000; //10 minutes
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            if (workerContext is null)
            {
                throw new ArgumentNullException(nameof(workerContext));
            }

            Logger.LogInformation("Starting: Attempting to geocode schools ...");

            //Resolve dependencies
            var schoolRepository = workerContext
                .ServiceProvider
                .GetRequiredService<ISchoolRepository>();

            var configuration = workerContext.ServiceProvider.GetRequiredService<IConfiguration>();

            // TODO: Geocode and save result
            var schoolsToGeocode = await schoolRepository.GetListAsync(isGeoLocated: false, maxResultCount: 5).ConfigureAwait(false);

            for (var i = 0; i < schoolsToGeocode.Count - 1; i++)
            {
                try
                {
                    var schoolToGeocode = schoolsToGeocode[i];
                    Logger.LogInformation("Starting: Attempting to geocode school {Id} ...", schoolToGeocode.Id);

                    var apiKey = configuration["App:GoogleMapsApiKey"];
                    IGeocoder geocoder = new GoogleGeocoder() { ApiKey = apiKey };

                    // Attempt at normalizing this data, removing whitespace that might mess up the result
                    var address = string.Empty;
                    if (!string.IsNullOrWhiteSpace(schoolToGeocode.Address1))
                    {
                        address += schoolToGeocode.Address1.Trim();
                    }
                    if (!string.IsNullOrWhiteSpace(schoolToGeocode.City))
                    {
                        address += $", {schoolToGeocode.City.Trim()}";
                    }
                    address += $", ON";
                    if (!string.IsNullOrWhiteSpace(schoolToGeocode.PostalCode))
                    {
                        address += $", {schoolToGeocode.PostalCode.Trim()}";
                    }

                    Logger.LogInformation("Attempting to geocode address '{Address}' ..", address);

                    var addresses = await geocoder.GeocodeAsync(address).ConfigureAwait(false);
                    if (addresses != null && addresses.Any())
                    {
                        var addressResult = addresses.First();

                        var lat = addressResult.Coordinates.Latitude;
                        var lng = addressResult.Coordinates.Longitude;

                        schoolToGeocode.Location = new Point(lng, lat) { SRID = SchoolConsts.SRID };

                        await schoolRepository.UpdateAsync(schoolToGeocode).ConfigureAwait(false);

                        Logger.LogInformation($"GEOCODE {i}: Address '{address}' = Lat {lat}, Long {lng}");
                    }
                    else
                    {
                        Logger.LogInformation($"GEOCODE {i}: No addresses returned for {address}.");
                    }
                }
                catch (GoogleGeocodingException gEx)
                {
                    Logger.LogException(gEx.Demystify());
                    if (gEx.InnerException != null)
                    {
                        Logger.LogException(gEx.InnerException.Demystify());
                    }
                    break;
                }
            }
        }
    }
}
