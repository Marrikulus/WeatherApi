namespace WeatherApi.Models;

public class ForecastData
{
    public string cod { get; set; }
    public int message { get; set; }
    public City city { get; set; }
    public int cnt { get; set; }
    public List<ForecastPoint> list { get; set; }
}