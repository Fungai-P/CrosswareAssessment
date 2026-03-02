using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Entities;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Handlers.Mappers;

public static class PostsMapper
{
    public static PostEntity Map(this CreatePostRequestDto request, string authorId)
    {
        return new PostEntity
        {
            AuthorId = authorId,
            Text = request.Text.Trim(),
            CreatedAt = DateTime.UtcNow,
            LikesCount = 0
        };
    }

    public static PostResult Map(this PostEntity entity)
    {
        return new PostResult
        {
            Id = entity.Id,
            AuthorId = entity.AuthorId,
            Text = entity.Text,
            CreatedAt = entity.CreatedAt,
            LikesCount = entity.LikesCount
        };
    }
}
