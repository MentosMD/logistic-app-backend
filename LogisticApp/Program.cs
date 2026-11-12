using Confluent.Kafka;
using LogisticApp;
using LogisticApp.AITools;
using LogisticApp.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LogisticsDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"));

var producerConfig = builder.Configuration
    .GetSection("KafkaProducer")
    .Get<ProducerConfig>();

builder.Services.AddSingleton<IProducer<string, string>>(sp =>
{
    return new ProducerBuilder<string, string>(producerConfig).Build();
});



builder.Services.AddTransient<IDriverService, DriverService>();
builder.Services.AddTransient<IVehiclesService, VehiclesService>();
builder.Services.AddTransient<IDocumentsService, DocumentsService>();
builder.Services.AddScoped<LogisticsAiTools>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Map the OpenAPI JSON endpoint
    app.MapOpenApi();
    // Map the Scalar API reference UI
    app.MapScalarApiReference();
}

app.UseCors();

app.MapMcp();
app.UseAuthorization();
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.MapControllers();

app.Run();
