using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Configuração do Entity Framework Core com SQL Server
builder.Services.AddDbContext<DeskFlow.API.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos Repositórios (Camada de Dados)
builder.Services.AddScoped<DeskFlow.API.Repositories.Interfaces.ICategoriaRepository, DeskFlow.API.Repositories.CategoriaRepository>();
builder.Services.AddScoped<DeskFlow.API.Repositories.Interfaces.IChamadoRepository, DeskFlow.API.Repositories.ChamadoRepository>();
builder.Services.AddScoped<DeskFlow.API.Repositories.Interfaces.IInteracaoRepository, DeskFlow.API.Repositories.InteracaoRepository>();

// Registro dos Serviços (Camada de Negócio)
builder.Services.AddScoped<DeskFlow.API.Services.Interfaces.ICategoriaService, DeskFlow.API.Services.CategoriaService>();
builder.Services.AddScoped<DeskFlow.API.Services.Interfaces.IChamadoService, DeskFlow.API.Services.ChamadoService>();




builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
