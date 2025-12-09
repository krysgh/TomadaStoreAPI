using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using RabbitMQ.Client;
using TomadaStore.SaleAPI.Repository;
using TomadaStore.SalesAPI.Data;
using TomadaStore.SalesAPI.Repositories.Interfaces;
using TomadaStore.SalesAPI.Services;
using TomadaStore.SalesAPI.Services.Interfaces;
using TomadaStore.SalesAPI.Services.Interfaces.v2;
using TomadaStore.SalesAPI.Services.v2;


var builder = WebApplication.CreateBuilder(args);

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

// Controllers
builder.Services.AddControllers();

// Isso aqui lê o bloco de MongoDB do appsettings.json pra conectar o mongo
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

// Registra o ConnectionDB
builder.Services.AddSingleton<ConnectionDB>();

// Repository e Service
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IProducerService, ProducerService>();

// HTTP Clients para comunicação com CustomerAPI e ProductAPI
builder.Services.AddHttpClient("CustomerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/v1/Customer/");
});

builder.Services.AddHttpClient("ProductAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:6001/api/v1/Product/");
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
