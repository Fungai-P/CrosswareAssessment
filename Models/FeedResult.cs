namespace CrosswareAssessment.Models;

public class FeedResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PostResult> Posts { get; set; }
}
