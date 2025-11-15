using MediatR;
using Microsoft.Azure.Cosmos;
using Content.Application.Queries.GetContent;
using Content.Domain.Repositories;
using Content.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetContentHandler).Assembly));

// Cosmos DB
builder.Services.AddSingleton<CosmosClient>(provider =>
{
    var connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    return new CosmosClient(connectionString);
});

// Repositories
builder.Services.AddScoped<IContentRepository, ContentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();