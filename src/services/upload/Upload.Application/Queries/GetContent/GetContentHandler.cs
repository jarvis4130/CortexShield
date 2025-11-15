using MediatR;
using Upload.Application.DTOs;
using Upload.Domain.Repositories;

namespace Upload.Application.Queries.GetContent;

public class GetContentHandler : IRequestHandler<GetContentQuery, GetContentResponse>
{
    private readonly IContentRepository _contentRepository;

    public GetContentHandler(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public async Task<GetContentResponse> Handle(GetContentQuery request, CancellationToken cancellationToken)
    {
        var content = await _contentRepository.GetByIdAsync(request.ContentId);
        
        if (content == null)
        {
            throw new KeyNotFoundException($"Content with ID {request.ContentId} not found");
        }

        return new GetContentResponse
        {
            ContentId = content.contentId,
            Type = content.Type,
            BlobUrl = content.BlobUrl,
            Status = content.Status,
            ContentHash = content.ContentHash,
            UploadedAt = content.UploadedAt
        };
    }
}