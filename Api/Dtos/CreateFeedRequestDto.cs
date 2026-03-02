using System.ComponentModel.DataAnnotations;

namespace CrosswareAssessment.Api.Dtos;

public class CreateFeedRequestDto
{
    [Required]
    public string Name { get; set; }
}
