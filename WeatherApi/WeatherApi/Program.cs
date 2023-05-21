using Microsoft.Extensions.DependencyInjection.Extensions;
using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<CityService>();
var app = builder.Build();
app.MapGet("/", () =>  Results.Redirect("/weather/Reykjavik"));
app.MapControllers();
app.Run();