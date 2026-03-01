using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Entities;
using CrosswareAssessment.Handlers.Mappers;
using CrosswareAssessment.Models;
using CrosswareAssessment.Repositories;

namespace CrosswareAssessment.Handlers;
public class FeedsHandler : IFeedsHandler
{
    private readonly IPostsRepository _postsRepository;
    private readonly IFeedsRepository _feedsRepository;

    public FeedsHandler(IPostsRepository postsRepository, IFeedsRepository feedsRepository)
    {
        _postsRepository = postsRepository;
        _feedsRepository = feedsRepository;
    }

    public async Task<FeedResult> CreateAsync(CreateFeedRequest request, CancellationToken ct)
    {
        var feed = new FeedEntity
        {
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };
        var result = await _feedsRepository.CreateAsync(feed, ct);

        return result.Map(null);
    }

    public async Task<FeedResult> GetAsync(string feedId, CancellationToken ct)
    {
        var feed = await _feedsRepository.GetByIdAsync(feedId, ct);
        if (feed is null) return null;

        var posts = await _postsRepository.GetByIdsAsync(feed.PostIds, ct);

        return feed.Map(posts);
    }

    public async Task<List<FeedResult>> GetAllAsync(CancellationToken ct)
    {
        var feeds = await _feedsRepository.ListAsync(ct);
        if (feeds is null || !feeds.Any()) return new List<FeedResult>();

        var allPostIds = feeds.SelectMany(x => x.PostIds).Distinct().ToList();
        var posts = await _postsRepository.GetByIdsAsync(allPostIds, ct);

        var postsDictionary = posts.ToDictionary(x => x.Id);

        return feeds.Select(
            feed => feed.Map(feed.PostIds.Where(postId => postsDictionary.ContainsKey(postId)).Select(postId => postsDictionary[postId]).ToList()
        )).ToList();
    }
}
