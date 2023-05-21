namespace WeatherApi.Models;

class GeoLocation
{
    public string name { get; set; }
    public Dictionary<string, string> local_names { get; set; }
    public float lat { get; set; }
    public float lon { get; set; }
    public string country { get; set; }
}