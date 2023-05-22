using Microsoft.Extensions.DependencyInjection.Extensions;
using WeatherApi;
using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<CityService>();
var app = builder.Build();
app.MapGet("/", () =>  Results.Redirect("/weather/Reykjavik/history/1989-10-09"));
app.MapControllers();
app.Run();