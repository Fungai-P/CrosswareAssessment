using System.ComponentModel.DataAnnotations;

namespace CrosswareAssessment.Api.Dtos;

public class CreateFeedRequest
{
    [Required]
    public string Name { get; set; }
}
