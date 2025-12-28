using FluentValidation;
using MediatR;

namespace CarAuction.Api.Features.WeatherForecast
{
    public record GetWeatherForecastQuery : IRequest<Domain.Entities.WeatherForecast[]>;

    public class GetWeatherForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
    {

    }

    internal sealed class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, Domain.Entities.WeatherForecast[]>
    {
        public async Task<Domain.Entities.WeatherForecast[]> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
                new Domain.Entities.WeatherForecast
                {

                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
                .ToArray();

            return forecast;
        }
    }

    public static class GetWeatherForecastEndpoint
    {
        public static void MapGetWeatherForecastEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", Handle)
                .WithName("GetWeatherForecast");
        }

        private static async Task<IResult> Handle(IMediator mediator)
        {
            var weatherForecast = await mediator.Send(new GetWeatherForecastQuery());

            if (weatherForecast == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(weatherForecast);
        }
    }
}
