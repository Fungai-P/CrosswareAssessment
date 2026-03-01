using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Api.Mappers;

public static class FeedsMapper
{
    public static FeedResponse Map(this FeedResult result)
    {
        return new FeedResponse
        {
            Id = result.Id,
            Name = result.Name,
            CreatedAt = result.CreatedAt,
            Posts = result.Posts.Select(p => p.Map()).ToList() ?? new List<PostResponse>()
        };
    }
}