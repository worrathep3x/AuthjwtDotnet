using Application.Handler.WeatherForecasts.Queries.GetWeather;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthenApi.Controllers;

namespace AuthenApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ApiControllerBase
    {
        /// <summary>
        /// WeatherReport
        /// </summary>
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<Response<IEnumerable<ModelWeather>>> GetWeatherForecasts()
        {
            return await Mediator.Send(new GetWeatherForecast());
        }
    }
}
