using MediatR;
using Microsoft.Azure.Cosmos;
using Audit.Application.Commands.CreateAuditLog;
using Audit.Application.Interfaces;
using Audit.Application.Services;
using Audit.Domain.Repositories;
using Audit.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAuditHandler).Assembly));

// Cosmos DB
builder.Services.AddSingleton<CosmosClient>(provider =>
{
    var connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    return new CosmosClient(connectionString);
});

// Repositories
builder.Services.AddScoped<IAuditRepository, AuditRepository>();

// Services
builder.Services.AddScoped<IAuditLogger, AuditLogger>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();