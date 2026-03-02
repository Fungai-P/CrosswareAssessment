namespace CrosswareAssessment.Api.Dtos;

public class FeedResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PostResponseDto> Posts { get; set; }
}