using StackExchange.Redis;
using Upload.Application.Interfaces;

namespace Upload.Infrastructure.Services;

public class DuplicateCheckService : IDuplicateCheckService
{
    private readonly IDatabase _database;

    public DuplicateCheckService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<bool> CheckDuplicateAsync(string contentHash)
    {
        return await _database.KeyExistsAsync(contentHash);
    }

    public async Task SetDuplicateAsync(string contentHash)
    {
        await _database.StringSetAsync(contentHash, "1", TimeSpan.FromDays(1));
    }
}