using CrosswareAssessment.Entities;
using MongoDB.Driver;

namespace CrosswareAssessment.Repositories;

public class LikesRepository : ILikesRepository
{
    private readonly IMongoCollection<LikeEntity> _likes;

    public LikesRepository(IMongoDatabase db)
    {
        _likes = db.GetCollection<LikeEntity>("likes");
    }

    public async Task<bool> ExistsAsync(string postId, string userId, CancellationToken ct)
    {
        return await _likes.Find(x => x.PostId == postId && x.UserId == userId)
          .AnyAsync(ct);
    }

    public async Task<bool> TryCreateAsync(string postId, string userId, CancellationToken ct)
    {
        try
        {
            var like = new LikeEntity
            {
                PostId = postId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _likes.InsertOneAsync(like, cancellationToken: ct);
            return true;
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            return false;
        }
    }
}
