using System.ComponentModel.DataAnnotations;

namespace CrosswareAssessment.Api.Dtos;

public class CreatePostRequest
{
    [Required]
    public string FeedId { get; set; }

    [Required]
    public string Text { get; set; }
}
