using MediatR;
using Content.Application.DTOs;
using Content.Domain.Repositories;

namespace Content.Application.Commands.UpdateContentStatus;

public class UpdateContentStatusHandler : IRequestHandler<UpdateContentStatusCommand, UpdatedResponse>
{
    private readonly IContentRepository _contentRepository;

    public UpdateContentStatusHandler(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public async Task<UpdatedResponse> Handle(UpdateContentStatusCommand request, CancellationToken cancellationToken)
    {
        var content = await _contentRepository.GetByIdAsync(request.ContentId);
        
        if (content == null)
        {
            throw new KeyNotFoundException($"Content with ID {request.ContentId} not found");
        }

        content.FinalStatus = request.Status;
        content.CombinedScore = request.CombinedScore;
        content.ConfidenceScore = request.ConfidenceScore;
        content.Priority = request.Priority;

        if (request.Status == "NeedsReview" && !string.IsNullOrEmpty(request.ReviewQueueId))
        {
            content.ReviewQueueId = request.ReviewQueueId;
        }
        else if (request.Status is "Approved" or "Blocked")
        {
            content.ReviewQueueId = request.ReviewQueueId;
        }

        await _contentRepository.UpdateAsync(content);

        return new UpdatedResponse(true);
    }
}