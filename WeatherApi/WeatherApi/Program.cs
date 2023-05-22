using Microsoft.OpenApi.Models;
using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<CityService>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Weather API",
        Description = "A simple weather api for current weather, forecast and history for a city",
        Contact = new OpenApiContact()
        {
            Name = "Ingimar Svanberg Jóhannesson",
            Email = "ingimar.johannesson@gmail.com",
        },
    });
});

var api_key = builder.Configuration["ApiKey"];
if (String.IsNullOrEmpty(api_key))
    throw new ArgumentException("Missing Api Key, Add ApiKey to configuration or environment variables. ");

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.Run();