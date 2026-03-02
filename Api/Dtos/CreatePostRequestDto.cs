using System.ComponentModel.DataAnnotations;

namespace CrosswareAssessment.Api.Dtos;

public class CreatePostRequestDto
{
    [Required]
    public string FeedId { get; set; }

    [Required]
    public string Text { get; set; }
}
