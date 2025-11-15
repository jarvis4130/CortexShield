using MediatR;
using Microsoft.Azure.Cosmos;
using Azure.Storage.Blobs;
using StackExchange.Redis;
using Upload.Application.Commands.InitUpload;
using Upload.Application.Interfaces;
using Upload.Domain.Repositories;
using Upload.Infrastructure.Repositories;
using Upload.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InitUploadHandler).Assembly));

// Cosmos DB
builder.Services.AddSingleton<CosmosClient>(provider =>
{
    var connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    return new CosmosClient(connectionString);
});

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
{
    return ConnectionMultiplexer.Connect("localhost:6379");
});

// Blob Storage
builder.Services.AddSingleton<BlobServiceClient>(provider =>
{
    return new BlobServiceClient("UseDevelopmentStorage=true");
});

// Services
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<IDuplicateCheckService, DuplicateCheckService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
