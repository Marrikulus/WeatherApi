namespace WeatherApi.Models;

public class ForecastData
{
    public string cod { get; set; }
    public int message { get; set; }
    public City city { get; set; }
    public int cnt { get; set; }
    public List<ForecastPoint> list { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class ForecastPoint
{
    public int dt { get; set; }
    public string dt_txt { get; set; }
    public int visibility { get; set; }
    
    //Probability of precipitation
    public float pop { get; set; }
    public WeatherMain main { get; set; }
    public List<WeatherDescription> weather { get; set; }
    public Clouds clouds { get; set; }
    public Wind wind { get; set; }
    public object rain { get; set; }
    public object sys { get; set; }
}