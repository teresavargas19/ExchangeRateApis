using ExchangeRateComparer.Services;
using ExchangeRateComparer.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddHttpClient();
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddScoped<IApiStrategy, Api1Strategy>();
builder.Services.AddScoped<IApiStrategy, Api2Strategy>();
builder.Services.AddScoped<IApiStrategy, Api3Strategy>();

var app = builder.Build();


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}





app.MapControllers();

app.Run();
