using MediatR;
using Content.Application.DTOs;
using Content.Domain.Repositories;

namespace Content.Application.Queries.GetContent;

public class GetContentHandler : IRequestHandler<GetContentQuery, ContentResponse>
{
    private readonly IContentRepository _contentRepository;

    public GetContentHandler(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public async Task<ContentResponse> Handle(GetContentQuery request, CancellationToken cancellationToken)
    {
        var content = await _contentRepository.GetByIdAsync(request.ContentId);
        
        if (content == null)
        {
            throw new KeyNotFoundException($"Content with ID {request.ContentId} not found");
        }

        return new ContentResponse(
            content.contentId,
            content.FinalStatus,
            content.CombinedScore,
            content.ConfidenceScore,
            content.Priority,
            content.ReviewQueueId
        );
    }
}