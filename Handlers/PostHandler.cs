using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Handlers.Mappers;
using CrosswareAssessment.Models;
using CrosswareAssessment.Repositories;

namespace CrosswareAssessment.Handlers;

public class PostHandler : IPostHandler
{
    private readonly IPostsRepository _postsRepository;
    private readonly IFeedsRepository _feedsRepository;

    public PostHandler(IPostsRepository postsRepository, IFeedsRepository feedsRepository)
    {
        _postsRepository = postsRepository;
        _feedsRepository = feedsRepository;
    }

    public async Task<PostResult> CreateAsync(string userId, CreatePostRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentException("Post text cannot be empty.");

        var feed = await _feedsRepository.GetByIdAsync(request.FeedId, ct);
        if (feed == null)
            throw new ArgumentException($"Feed with id {request.FeedId} not found.");

        var post = request.Map(userId);

        var result = await _postsRepository.CreateAsync(post, ct);

        feed.PostIds.Add(result.Id);
        await _feedsRepository.UpdateAsync(feed.Id, feed,  ct);

        return result.Map();
    }

    public async Task<PostResult> GetAsync(string postId, CancellationToken ct)
    {
        var result = await _postsRepository.GetByIdAsync(postId, ct);

        return result.Map();
    }
}
