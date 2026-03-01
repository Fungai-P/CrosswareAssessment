using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Api.Mappers;
using CrosswareAssessment.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("posts")]
public sealed class PostsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
      [FromBody] CreatePostRequest request,
      [FromServices] IPostHandler handler,
      CancellationToken ct)
    {
        // Minimal demo-friendly auth: user comes from header
        var userId = Request.Headers["X-User-Id"].ToString();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Missing/invalid X-User-Id header." });

        var created = await handler.CreateAsync(userId, request, ct);

        return Ok(created.Map());
    }

    [HttpGet("{postId:guid}")]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
      [FromRoute] string postId,
      [FromServices] IPostHandler handler,
      CancellationToken ct)
    {
        var post = await handler.GetAsync(postId, ct);
        if (post is null) return NotFound();

        return Ok(post.Map());
    }

    [HttpPost("{postId:guid}/like")]
    [ProducesResponseType(typeof(LikeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Like(
      [FromRoute] string postId,
      [FromServices] ILikesHandler handler,
      CancellationToken ct)
    {
        // Minimal demo-friendly auth: user comes from header
        var userId = Request.Headers["X-User-Id"].ToString();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Missing/invalid X-User-Id header." });

        var result = await handler.AddLikeAsync(postId, userId, ct);

        if (!result.Success)
            return Conflict(new { message = result.Message });

        return Ok(result.Map());
    }

    [HttpGet("{postId:guid}/likes/count")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLikeCount(
      [FromRoute] string postId,
      [FromServices] ILikesHandler handler,
      CancellationToken ct)
    {
        var count = await handler.LikesCountAsync(postId, ct);
        return Ok(count.Map());
    }
}