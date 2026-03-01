using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Handlers;

public interface IPostHandler
{
    Task<PostResult> CreateAsync(string userId, CreatePostRequest request, CancellationToken ct);
    Task<PostResult> GetAsync(string postId, CancellationToken ct);
}
