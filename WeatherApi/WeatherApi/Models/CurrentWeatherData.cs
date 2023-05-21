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
    public object sys { get; set; }
    public object clouds { get; set; }
    public Coord coord { get; set; }
    public object weather { get; set; }

}

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