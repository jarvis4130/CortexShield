using Microsoft.Azure.Cosmos;
using Content.Domain.Repositories;

namespace Content.Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly Container _container;

    public ContentRepository(CosmosClient cosmosClient)
    {
        var database = cosmosClient.CreateDatabaseIfNotExistsAsync("ContentDb").Result;
        _container = database.Database.CreateContainerIfNotExistsAsync("Content", "/contentId").Result;
    }

    public async Task<Domain.Entities.Content?> GetByIdAsync(string contentId)
    {
        try
        {
            var response = await _container.ReadItemAsync<Domain.Entities.Content>(contentId, new PartitionKey(contentId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task UpdateAsync(Domain.Entities.Content content)
    {
        await _container.ReplaceItemAsync(content, content.id, new PartitionKey(content.contentId));
    }
}