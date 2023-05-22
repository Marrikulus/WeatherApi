namespace WeatherApi.Models;

public class CurrentWeatherData
{
    public int id { get; set; }
    public string name { get; set; }
    public WeatherMain main { get; set; }
    public Wind wind { get; set; }
    public string @base { get; set; }
    public int visibility { get; set; }
    public int timezone { get; set; }
    public int cod { get; set; }
    public int dt { get; set; }
    public Clouds clouds { get; set; }
    public Coord coord { get; set; }
    public List<WeatherDescription> weather { get; set; }
    public object sys { get; set; }
}