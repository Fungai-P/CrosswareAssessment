using CrosswareAssessment.Models;

namespace CrosswareAssessment.Handlers;

public interface ILikesHandler
{
    Task<LikeResult> AddLikeAsync(string postId, string userId, CancellationToken ct);
    Task<LikesCountResult> LikesCountAsync(string postId, CancellationToken ct);
}
