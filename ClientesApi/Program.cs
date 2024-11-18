using ClientesApi.Data;
using ClientesApi.Interfaces;
using ClientesApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Logger(l =>
    l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Error)
    .WriteTo.File("Logs/Log-Error-.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l =>
    l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Information)
    .WriteTo.File("Logs/Log-.txt", rollingInterval: RollingInterval.Day))
    .CreateLogger();

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbContext")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddScoped<IClienteService, ClienteService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
