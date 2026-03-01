namespace CrosswareAssessment.Models;

public class LikeResult
{
    public string PostId { get; set; }
    public string UserId { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public int LikesCount { get; set; }
}
