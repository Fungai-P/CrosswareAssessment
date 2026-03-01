using MongoDB.Bson.Serialization.Attributes;

namespace CrosswareAssessment.Entities;

public class PostEntity
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string AuthorId { get; set; }
    public string Text {  get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int LikesCount { get; set; }
}
