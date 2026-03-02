using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Api.Mappers;

public static class PostsMapper
{
    public static PostResponseDto Map(this PostResult result)
    {
        return new PostResponseDto
        {
            Id = result.Id,
            AuthorId = result.AuthorId,
            CreatedAt = result.CreatedAt,
            LikesCount = result.LikesCount,
            Text = result.Text
        };
    }

    public static LikeResponseDto Map(this LikeResult result)
    {
        return new LikeResponseDto
        {
            PostId = result.PostId,
            UserId = result.UserId,
            Success = result.Success,
            Message = result.Message,
            LikesCount = result.LikesCount            
        };
    }

    public static LikesCountResponseDto Map(this LikesCountResult result)
    {
        return new LikesCountResponseDto
        {
            PostId = result.PostId,
            LikesCount = result.LikesCount
        };
    }
}
