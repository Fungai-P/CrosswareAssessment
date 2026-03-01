namespace CrosswareAssessment.Api.Dtos;

public class FeedResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PostResponse> Posts { get; set; }
}