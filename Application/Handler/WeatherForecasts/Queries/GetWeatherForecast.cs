using Application.Model;
using MediatR;

namespace Application.Handler.WeatherForecasts.Queries.GetWeather;

public record GetWeatherForecast : IRequest<Response<IEnumerable<ModelWeather>>>
{
}
public class GetWeatherForecasthandler : IRequestHandler<GetWeatherForecast, Response<IEnumerable<ModelWeather>>>
{
    private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public async Task<Response<IEnumerable<ModelWeather>>> Handle(GetWeatherForecast request, CancellationToken cancellationToken)
    {

        var rng = new Random();
        var result = Enumerable.Range(1, 5).Select(index => new ModelWeather
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return Response<IEnumerable<ModelWeather>>.SuccessResult(result);
    }
}
