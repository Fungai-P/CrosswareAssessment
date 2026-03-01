using CrosswareAssessment.Entities;
using MongoDB.Driver;

namespace CrosswareAssessment.Infrastructure;

public class MongoIndexesHostedService : IHostedService
{
    private readonly IMongoDatabase _db;

    public MongoIndexesHostedService(IMongoDatabase db) => _db = db;

    public async Task StartAsync(CancellationToken ct)
    {
        var likes = _db.GetCollection<LikeEntity>("likes");

        var keys = Builders<LikeEntity>.IndexKeys
          .Ascending(x => x.PostId)
          .Ascending(x => x.UserId);

        var model = new CreateIndexModel<LikeEntity>(
          keys,
          new CreateIndexOptions { Unique = true, Name = "unique_likes_post_user" }
        );

        await likes.Indexes.CreateOneAsync(model, cancellationToken: ct);
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}