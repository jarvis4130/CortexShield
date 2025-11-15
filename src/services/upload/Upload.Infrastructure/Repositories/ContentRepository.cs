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

    public async Task<Content?> GetByIdAsync(string contentId)
    {
        try
        {
            var response = await _container.ReadItemAsync<Content>(contentId, new PartitionKey(contentId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task UpdateAsync(Content content)
    {
        await _container.ReplaceItemAsync(content, content.id, new PartitionKey(content.contentId));
    }
}