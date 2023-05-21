using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : Controller
{
    private const string API_KEY = "e248d704018761b2a3d657e463ce47be";
    private HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri("http://api.openweathermap.org")
    };

    private readonly CityService _cityService;

    public WeatherController([FromServices] CityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Hello World");
    }

    async Task<Coord?> GetCityLocation(string cityName)
    {
        var city = _cityService.FindCityByName(cityName);
        if (city is not null)
        {
            return city.coord;
        }

        var geoResponse = await _client.GetAsync($"/geo/1.0/direct?q={cityName}&limit=1&appid={API_KEY}");
        var list = await geoResponse.Content.ReadFromJsonAsync<List<GeoLocation>>();
        if (list is null || list.Count < 1)
        {
            return null;
        }
        var geoLocation = list[0];
        return new Coord() {lon = geoLocation.lon, lat = geoLocation.lat};
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetCity(string city)
    {
        var location = await GetCityLocation(city);
        if (location is null)
        {
            return NotFound("City not found or valid");
        }
        
        var weatherResponse = await _client.GetAsync($"/data/2.5/weather?lat={location.lat}&lon={location.lon}&appid={API_KEY}");
        var weatherData = await weatherResponse.Content.ReadFromJsonAsync<CurrentWeatherData>();
        if (weatherData is null)
        {
            return NotFound("No weather for location");
        }

        return Ok(weatherData);
    }
    
    [HttpGet("{city}/forecast")]
    public async Task<IActionResult> GetCityForecast(string city)
    {
        var location = await GetCityLocation(city);
        if (location is null)
        {
            return NotFound("City not found or valid");
        }
        var weatherResponse = await _client.GetAsync($"/data/2.5/forecast?lat={location.lat}&lon={location.lon}&appid={API_KEY}");
        var weatherData = await weatherResponse.Content.ReadFromJsonAsync<ForecastData>();
        if (weatherData is null)
        {
            return NotFound("No weather for location");
        }

        return Ok(weatherData);
    }
    
    [HttpGet("{city}/history/{date:datetime}")]
    public async Task<IActionResult> GetCityHistory(string city, DateTime date)
    {
        var location = await GetCityLocation(city);
        if (location is null)
        {
            return NotFound("City not found or valid");
        }
        var weatherResponse = await _client.GetAsync($"/data/2.5/forecast?lat={location.lat}&lon={location.lon}&appid={API_KEY}");
        var weatherData = await weatherResponse.Content.ReadFromJsonAsync<HistoricalWeatherData>();
        if (weatherData is null)
        {
            return NotFound("No weather for location");
        }
        return Ok(weatherData);
    }
}