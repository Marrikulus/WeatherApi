namespace WeatherApi.Models;

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