namespace CrosswareAssessment.Repositories;

public interface ILikesRepository
{
    Task<bool> TryCreateAsync(string postId, string userId, CancellationToken ct);
    Task<bool> ExistsAsync(string postId, string userId, CancellationToken ct);
}
