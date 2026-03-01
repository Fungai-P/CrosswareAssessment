using CrosswareAssessment.Models;
using CrosswareAssessment.Repositories;
using CrosswareAssessment.Services;

namespace CrosswareAssessment.Handlers;

public class LikesHandler : ILikesHandler
{
    private readonly IPostsRepository _postsRepository;
    private readonly ILikesRepository _likesRepository;
    private readonly ICacheService _cacheService;

    public LikesHandler(IPostsRepository postsRepository, ILikesRepository likesRepository, ICacheService cacheService)
    {
        _postsRepository = postsRepository;
        _likesRepository = likesRepository;
        _cacheService = cacheService;
    }

    public async Task<LikeResult> AddLikeAsync(string postId, string userId, CancellationToken ct)
    {
        var post = await _postsRepository.GetByIdAsync(postId, ct);
        if (post is null) throw new InvalidOperationException("Post not found.");

        var inserted = await _likesRepository.TryCreateAsync(postId, userId, ct);

        int count;
        if (inserted)
        {
            count = await _postsRepository.IncrementLikeCountAsync(postId, +1, ct);

            // Set in cache
            await _cacheService.SetCache(postId, count, ct);
        }
        else
        {
            // already liked
            var cacheCount = await _cacheService.GetFromCacheAsync(postId, ct);
            // read count from cache if available
            count = (cacheCount != null) ? (int)cacheCount : await _postsRepository.GetLikeCountAsync(postId, ct);
        }
        return new LikeResult
        {
            PostId = postId,
            UserId = userId,
            Success = inserted,
            Message = (!inserted) ? "Already liked." : string.Empty,
            LikesCount = count
        };
    }

    public async Task<LikesCountResult> LikesCountAsync(string postId, CancellationToken ct)
    {
        var post = await _postsRepository.GetByIdAsync(postId, ct);
        if (post is null) return null;

        // read count from cache if available
        var count = await _cacheService.GetFromCacheAsync(postId, ct);
        if (count == null)
        {
            count = await _postsRepository.GetLikeCountAsync(postId, ct);
            
            // Set in cache
            await _cacheService.SetCache(postId, (int) count, ct);
        }

        return new LikesCountResult
        {
            PostId = postId,
            LikesCount = (int) count
        };
    }
}
