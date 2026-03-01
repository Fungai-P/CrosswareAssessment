using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Api.Mappers;
using CrosswareAssessment.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("feeds")]
public sealed class FeedsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
      [FromBody] CreateFeedRequest request,
      [FromServices] IFeedsHandler handler,
      CancellationToken ct)
    {
        var created = await handler.CreateAsync(request, ct);

        return Ok(created.Map());
    }

    [HttpGet("{feedId:guid}")]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
      [FromRoute] string feedId,
      [FromServices] IFeedsHandler handler,
      CancellationToken ct)
    {
        var feed = await handler.GetAsync(feedId, ct);
        if (feed is null) return NotFound();

        return Ok(feed.Map());
    }

    [HttpGet]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList(
      [FromServices] IFeedsHandler handler,
      CancellationToken ct)
    {
        var feeds = await handler.GetAllAsync(ct);

        return Ok(feeds.Select(x => x.Map()));
    }
}
