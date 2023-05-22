using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : Controller
{
    private HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri("http://api.openweathermap.org")
    };

    private readonly CityService _cityService;
    private readonly string _apiKey;

    public WeatherController([FromServices] CityService cityService, IConfiguration config)
    {
        _cityService = cityService;
        _apiKey = config["ApiKey"];
    }

    private async Task<Coord?> GetCityLocation(string cityName)
    {
        var city = _cityService.FindCityByName(cityName);
        if (city is not null)
        {
            return city.coord;
        }

        var geoResponse = await _client.GetAsync($"/geo/1.0/direct?q={cityName}&limit=1&appid={_apiKey}");
        var list = await geoResponse.Content.ReadFromJsonAsync<List<GeoLocation>>();
        if (list is null || list.Count < 1)
        {
            return null;
        }
        var geoLocation = list[0];
        return new Coord() {lon = geoLocation.lon, lat = geoLocation.lat};
    }

    /// <summary>
    /// Return the current weather for a city
    /// </summary>
    /// <param name="city" example="Reykjavík"></param>
    /// <response code="200">Returns the current weather for a city</response>
    /// <response code="400">Not a valid city name, or city does not exist</response>
    /// <response code="404">Valid city but weather not found for city</response>
    [HttpGet("{city}")]
    [ProducesResponseType(typeof(CurrentWeatherData),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCity(string city)
    {
        var location = await GetCityLocation(city);
        if (location is null)
        {
            return ValidationProblem("City not found or valid");
        }

        var weatherResponse = await _client.GetAsync($"/data/2.5/weather?units=metric&lat={location.lat}&lon={location.lon}&appid={_apiKey}");
        var weatherData = await weatherResponse.Content.ReadFromJsonAsync<CurrentWeatherData>();
        if (weatherData is null)
        {
            return NotFound("No weather for location");
        }

        return Ok(weatherData);
    }
    
    /// <summary>
    /// Return the 5 day forecast for a city
    /// </summary>
    /// <param name="city" example="Reykjavík"></param>
    /// <response code="200">Returns the forecasted weather for a city</response>
    /// <response code="400">Not a valid city name, or city does not exist</response>
    /// <response code="404">Valid city but weather not found for city</response>
    [HttpGet("{city}/forecast")]
    [ProducesResponseType(typeof(ForecastData),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCityForecast(string city)
    {
        var location = await GetCityLocation(city);
        if (location is null)
        {
            return ValidationProblem("City not found or valid");
        }
        var weatherResponse = await _client.GetAsync($"/data/2.5/forecast?units=metric&lat={location.lat}&lon={location.lon}&appid={_apiKey}");
        var weatherData = await weatherResponse.Content.ReadFromJsonAsync<ForecastData>();
        if (weatherData is null)
        {
            return NotFound("No weather for location");
        }

        return Ok(weatherData);
    }
    
    /// <summary>
    /// Return Historical weather data for a day in a city
    /// </summary>
    /// <param name="city" example="Reykjavík"></param>
    /// <param name="date" example="2023-01-01"></param>
    /// <response code="200">Returns the historical weather for a city</response>
    /// <response code="400">Not a valid city name, or city does not exist or date is outside valid range</response>
    /// <response code="404">weather not found for city and date</response>
    [HttpGet("{city}/history/{date:datetime}")]
    [ProducesResponseType(typeof(HistoricalWeatherData),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCityHistory(string city, DateTime date)
    {
        var location = await GetCityLocation(city);
        if (location is null)
        {
            return ValidationProblem("City not found or valid");
        }
        
        var timestamp = ((DateTimeOffset)date).ToUnixTimeSeconds();
        long beginningOfData = 283996800;
        if (timestamp < beginningOfData)
        {
            return ValidationProblem("Date to old, History data only goes back to January 1 1979");
        }

        if (DateTime.Now < date)
        {
            return ValidationProblem("Date to old, History data only goes back to January 1 1979");
        }

        var weatherResponse = await _client.GetAsync($"/data/3.0/onecall/timemachine?units=metric&dt={timestamp}&lat={location.lat}&lon={location.lon}&appid={_apiKey}");
        var weatherData = await weatherResponse.Content.ReadFromJsonAsync<HistoricalWeatherData>();
        if (weatherData is null)
        {
            return NotFound("No weather for location");
        }
        return Ok(weatherData);
    }
}