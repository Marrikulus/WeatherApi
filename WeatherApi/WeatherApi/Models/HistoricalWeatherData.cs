namespace WeatherApi.Models;

public class HistoricalWeatherData
{
    public float lat { get; set; }
    public float lon { get; set; }
    public string timezone { get; set; }
    public int timezone_offset { get; set; }
    public List<HistoryDataPoint> data { get; set; }
}