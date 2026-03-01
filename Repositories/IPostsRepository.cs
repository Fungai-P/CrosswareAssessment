using CrosswareAssessment.Entities;

namespace CrosswareAssessment.Repositories;

public interface IPostsRepository
{
    Task<PostEntity> CreateAsync(PostEntity post, CancellationToken ct);
    Task<PostEntity?> GetByIdAsync(string postId, CancellationToken ct);
    Task<int> GetLikeCountAsync(string postId, CancellationToken ct);
    Task<int> IncrementLikeCountAsync(string postId, long delta, CancellationToken ct);
    Task<List<PostEntity>> GetByIdsAsync(IEnumerable<string> postIds, CancellationToken ct);
}
