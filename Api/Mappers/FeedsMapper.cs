using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Api.Mappers;

public static class FeedsMapper
{
    public static FeedResponseDto Map(this FeedResult result)
    {
        return new FeedResponseDto
        {
            Id = result.Id,
            Name = result.Name,
            CreatedAt = result.CreatedAt,
            Posts = result.Posts.Select(p => p.Map()).ToList() ?? new List<PostResponseDto>()
        };
    }
}