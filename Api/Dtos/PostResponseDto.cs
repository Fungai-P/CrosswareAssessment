namespace CrosswareAssessment.Api.Dtos;

public class PostResponseDto
{
    public string Id { get; set; }
    public string AuthorId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public long LikesCount { get; set; }
}
