using Asp.Versioning;
using CarAuction.Api.Common.Interfaces;

namespace CarAuction.Api.Features.WeatherForecast
{
    public class _WeatherForecastFeature : IFeature
    {
        public const string RoutePrefix = "api/v{version:apiVersion}/weather-forecast";
        public const string Tag = "WeatherForecast";

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var versionedApi = app.NewVersionedApi();

            var group = versionedApi
                .MapGroup(RoutePrefix)
                .WithTags(Tag)
                .HasApiVersion(new ApiVersion(1, 0));

            group.MapGetWeatherForecastEndpoint();
        }
    }
}
