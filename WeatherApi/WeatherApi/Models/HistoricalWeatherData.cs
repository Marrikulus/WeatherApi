namespace WeatherApi.Models;

public class HistoricalWeatherData
{
    public float lat { get; set; }
    public float lon { get; set; }
    public string timezone { get; set; }
    public int timezone_offset { get; set; }

    public List<WeatherDataPoint> data { get; set; }

    public class WeatherDataPoint {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public float temp { get; set; }
        public float feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float dew_point { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public int wind_speed { get; set; }
        public int wind_deg { get; set; }
        public List<WeatherDescription> weather { get; set; }
        
    }
}