using CrosswareAssessment.Api.Dtos;
using CrosswareAssessment.Models;

namespace CrosswareAssessment.Handlers;

public interface IFeedsHandler
{
    Task<FeedResult> CreateAsync(CreateFeedRequestDto request, CancellationToken ct);
    Task<FeedResult> GetAsync(string feedId, CancellationToken ct);
    Task<List<FeedResult>> GetAllAsync(CancellationToken ct);
}
