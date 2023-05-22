namespace WeatherApi.Models;

public class WeatherMain
{
    public float temp { get; set; }
    public float feels_like { get; set; }
    public float temp_min { get; set; }
    public float temp_max { get; set; }
    public float pressure { get; set; }
    public float humidity { get; set; }
    public float sea_level { get; set; }
    public float grnd_level { get; set; }
    public float temp_kf { get; set; }
}