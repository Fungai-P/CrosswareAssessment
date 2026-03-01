namespace CrosswareAssessment.Services;

public interface ICacheService
{
    Task<int?> GetFromCacheAsync(string postId, CancellationToken ct);
    Task SetCache(string postId, int count, CancellationToken ct);
}
