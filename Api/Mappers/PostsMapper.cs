using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Api.Mappers;

public static class PostsMapper
{
    public static PostResponse Map(this PostResult result)
    {
        return new PostResponse
        {
            Id = result.Id,
            AuthorId = result.AuthorId,
            CreatedAt = result.CreatedAt,
            LikesCount = result.LikesCount,
            Text = result.Text
        };
    }

    public static LikeResponse Map(this LikeResult result)
    {
        return new LikeResponse
        {
            PostId = result.PostId,
            UserId = result.UserId,
            Success = result.Success,
            Message = result.Message,
            LikesCount = result.LikesCount            
        };
    }

    public static LikesCountResponse Map(this LikesCountResult result)
    {
        return new LikesCountResponse
        {
            PostId = result.PostId,
            LikesCount = result.LikesCount
        };
    }
}
