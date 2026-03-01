using CrosswareAssessment.Entities;

namespace CrosswareAssessment.Repositories;

public interface IFeedsRepository
{
    Task<FeedEntity> CreateAsync(FeedEntity feed, CancellationToken ct);
    Task<FeedEntity?> GetByIdAsync(string feedId, CancellationToken ct);
    Task<List<FeedEntity>> ListAsync(CancellationToken ct);
    Task<FeedEntity?> UpdateAsync(string feedId, FeedEntity entity, CancellationToken ct);
}
