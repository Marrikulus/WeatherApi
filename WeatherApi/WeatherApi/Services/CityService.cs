using System.Collections.Concurrent;
using System.Security;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WeatherApi.Models;

namespace WeatherApi.Services;

public class CityService
{
    private ConcurrentDictionary<string, City> Cities { get; set; }

    public CityService()
    {
        Cities = new ConcurrentDictionary<string, City>();
        Load();
    }

    private static string normalize_string(string str)
    {
        return str.ToLower().RemoveDiacritics();
    }

    public City? FindCityByName(string name)
    {
        var normalizedName = normalize_string(name);
        if (Cities.ContainsKey(normalizedName))
        {
            return Cities[normalizedName];
        }

        return null;
    }

    public void Load()
    {
        var json = File.ReadAllText("city.list.json");
        var listOfCities = JsonConvert.DeserializeObject<List<City>>(json);
        if (listOfCities == null) return;
        foreach (var city in listOfCities)
        {
            Cities.TryAdd(normalize_string(city.name), city);
        }
    }
}