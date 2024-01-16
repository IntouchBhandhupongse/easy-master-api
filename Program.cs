using easy_master_api.Class;
using Microsoft.EntityFrameworkCore;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DBContext>(opt =>{
  opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
    builder.WithOrigins(configuration["AppSettings:AllowedOrigin"])
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod());

app.Use(async (contexts, next) =>
{
    //http or https
    contexts.Request.Scheme = "http";

    contexts.Request.Headers.Add("X-Xss-Protection", "1; mode=block");
    contexts.Request.Headers.Add("X-Content-Type-Options", "nosniff");

    contexts.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
    contexts.Response.Headers.Add("X-Content-Type-Options", "nosniff");

    await next();
});

app.UseHttpsRedirection();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
