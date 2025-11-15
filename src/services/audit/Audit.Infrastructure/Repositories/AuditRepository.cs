using Microsoft.Azure.Cosmos;
using Audit.Domain.Entities;
using Audit.Domain.Repositories;

namespace Audit.Infrastructure.Repositories;

public class AuditRepository : IAuditRepository
{
    private readonly Container _container;

    public AuditRepository(CosmosClient cosmosClient)
    {
        var database = cosmosClient.CreateDatabaseIfNotExistsAsync("mydb").Result;
        _container = database.Database.CreateContainerIfNotExistsAsync("AuditLogs", "/EntityId").Result;
    }

    public async Task SaveAsync(AuditLog log)
    {
        await _container.CreateItemAsync(log, new PartitionKey(log.EntityId));
    }

    public async Task<List<AuditLog>> GetByEntityIdAsync(string entityId)
    {
        var query = new QueryDefinition("SELECT * FROM c WHERE c.EntityId = @entityId ORDER BY c.Timestamp DESC")
            .WithParameter("@entityId", entityId);

        var iterator = _container.GetItemQueryIterator<AuditLog>(query, requestOptions: new QueryRequestOptions
        {
            PartitionKey = new PartitionKey(entityId)
        });

        var results = new List<AuditLog>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}