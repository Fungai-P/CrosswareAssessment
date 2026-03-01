using CrosswareAssessment.Entities;
using MongoDB.Driver;

namespace CrosswareAssessment.Repositories;

public sealed class FeedsRepository : IFeedsRepository
{
    private readonly IMongoCollection<FeedEntity> _feeds;
    private readonly IPostsRepository _posts;

    public FeedsRepository(IMongoDatabase db, IPostsRepository posts)
    {
        _feeds = db.GetCollection<FeedEntity>("feeds");
        _posts = posts;
    }

    public async Task<FeedEntity> CreateAsync(FeedEntity feed, CancellationToken ct)
    {
        feed.Id ??= Guid.NewGuid().ToString("N");
        feed.CreatedAt = feed.CreatedAt == default ? DateTime.UtcNow : feed.CreatedAt;

        feed.PostIds ??= new List<string>();

        await _feeds.InsertOneAsync(feed, cancellationToken: ct);
        return feed;
    }

    public async Task<FeedEntity?> GetByIdAsync(string feedId, CancellationToken ct)
    {
        return await _feeds.Find(x => x.Id == feedId).FirstOrDefaultAsync(ct);
    }

    public async Task<List<FeedEntity>> ListAsync(CancellationToken ct)
    {
        return await _feeds.Find(FilterDefinition<FeedEntity>.Empty)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<FeedEntity?> UpdateAsync(
        string feedId,
        FeedEntity entity,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(feedId))
            throw new ArgumentException("FeedId is required.");

        var filter = Builders<FeedEntity>.Filter.Eq(x => x.Id, feedId);

        var update = Builders<FeedEntity>.Update
            .Set(x => x.PostIds, entity.PostIds);

        var options = new FindOneAndUpdateOptions<FeedEntity>
        {
            ReturnDocument = ReturnDocument.After
        };

        var updatedFeed = await _feeds.FindOneAndUpdateAsync(filter, update, options, ct);

        if (updatedFeed is null)
            return null;

        return updatedFeed;
    }
}
