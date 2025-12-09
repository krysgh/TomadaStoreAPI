
using MongoDB.Driver;
using RabbitMQ.Client;
using TomadaStore.Consumer.Repositories.Interfaces.v2;
using TomadaStore.Consumer.Repositories.v2;
using TomadaStore.Consumer.Services.Interfaces.v2;
using TomadaStore.Consumer.Services.v2;
using TomadaStore.SaleAPI.Repository;
using TomadaStore.SalesAPI.Data;
using TomadaStore.SalesAPI.Repositories.Interfaces;
using TomadaStore.SalesAPI.Services;
using TomadaStore.SalesAPI.Services.Interfaces;
using TomadaStore.SalesAPI.Services.Interfaces.v2;
using TomadaStore.SalesAPI.Services.v2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    return new ConnectionFactory()
    {
        HostName = "localhost",
        Port = 5672,
        UserName = "guest",
        Password = "guest"
    };
});

builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddScoped<IConsumerService, ConsumerService>();

builder.Services.AddScoped<IConsumerRepository, ConsumerRepository>();


builder.Services.AddHttpClient("CustomerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/v1/Customer/");
});

builder.Services.AddHttpClient("ProductAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:6001/api/v1/Product/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
