using CrosswareAssessment.Entities;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Handlers.Mappers
{
    public static class FeedsMapper
    {
        public static FeedResult Map(this FeedEntity entity, List<PostEntity> posts)
        {
            return new FeedResult
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                Posts = posts?.Select(p => p.Map()).ToList() ?? new List<PostResult>()
            };
        }
    }
}
