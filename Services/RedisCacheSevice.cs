namespace CrosswareAssessment.Services;

using StackExchange.Redis;

public sealed class RedisCacheSevice : ICacheService
{
    private readonly IDatabase _cache;

    public RedisCacheSevice(IConnectionMultiplexer multiplexer)
    {
        _cache = multiplexer.GetDatabase();
    }

    private static string Key(string postId) => $"likeCount:{postId}";

    public async Task<int?> GetFromCacheAsync(string postId, CancellationToken ct)
    {
        var value = await _cache.StringGetAsync(Key(postId));
        if (!value.HasValue) return null;
        return (int)value;
    }

    public Task SetCache(string postId, int count, CancellationToken ct)
    {
        return _cache.StringSetAsync(Key(postId), count);
    }
}
