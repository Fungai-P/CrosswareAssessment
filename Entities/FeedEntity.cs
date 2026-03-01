using MongoDB.Bson.Serialization.Attributes;

namespace CrosswareAssessment.Entities;

public class FeedEntity
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<string> PostIds { get; set; } = new();
}
