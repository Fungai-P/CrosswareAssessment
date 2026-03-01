using CrosswareAssessment.Entities;
using MongoDB.Driver;

namespace CrosswareAssessment.Repositories;

public class PostsRepository : IPostsRepository
{
    private readonly IMongoCollection<PostEntity> _posts;

    public PostsRepository(IMongoDatabase db)
    {
        _posts = db.GetCollection<PostEntity>("posts");
    }

    public async Task<PostEntity> CreateAsync(PostEntity post, CancellationToken ct)
    {
        await _posts.InsertOneAsync(post, cancellationToken: ct);
        return post;
    }

    public async Task<PostEntity?> GetByIdAsync(string postId, CancellationToken ct)
    {
        return await _posts.Find(x => x.Id == postId).FirstOrDefaultAsync(ct);
    }

    public async Task<int> GetLikeCountAsync(string postId, CancellationToken ct)
    {
        var projection = Builders<PostEntity>.Projection.Expression(x => x.LikesCount);

        var count = await _posts.Find(x => x.Id == postId)
          .Project(projection)
          .FirstOrDefaultAsync(ct);

        return count;
    }

    public async Task<int> IncrementLikeCountAsync(string postId, long delta, CancellationToken ct)
    {
        var filter = Builders<PostEntity>.Filter.Eq(x => x.Id, postId);
        var update = Builders<PostEntity>.Update
          .Inc(x => x.LikesCount, delta);

        var options = new FindOneAndUpdateOptions<PostEntity>
        {
            IsUpsert = false,
            ReturnDocument = ReturnDocument.After,
            Projection = Builders<PostEntity>.Projection.Include(x => x.LikesCount)
        };

        var updated = await _posts.FindOneAndUpdateAsync(filter, update, options, ct);

        if (updated is null)
            throw new InvalidOperationException($"Post {postId} not found.");

        return updated.LikesCount;
    }

    public async Task<List<PostEntity>> GetByIdsAsync(IEnumerable<string> postIds, CancellationToken ct)
    {
        var ids = postIds?.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList() ?? new();
        if (ids.Count == 0) return new List<PostEntity>();

        var filter = Builders<PostEntity>.Filter.In(x => x.Id, ids);
        var posts = await _posts.Find(filter).ToListAsync(ct);

        var map = posts.ToDictionary(p => p.Id, p => p);
        return ids.Where(map.ContainsKey).Select(id => map[id]).ToList();
    }
}
