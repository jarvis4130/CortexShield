using Microsoft.Azure.Cosmos;
using Upload.Domain.Entities;
using Upload.Domain.Repositories;

namespace Upload.Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly Container _container;

    public ContentRepository(CosmosClient cosmosClient)
    {
        var database = cosmosClient.CreateDatabaseIfNotExistsAsync("ContentDb").Result;
        _container = database.Database.CreateContainerIfNotExistsAsync("Content", "/contentId").Result;
    }

    public async Task CreateAsync(Content content)
    {
        await _container.CreateItemAsync(content, new PartitionKey(content.contentId));
    }
}