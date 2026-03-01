using MongoDB.Bson.Serialization.Attributes;

namespace CrosswareAssessment.Entities;

public class LikeEntity
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string PostId { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
