using MediatR;
using Upload.Application.DTOs;
using Upload.Domain.Repositories;

namespace Upload.Application.Commands.CompleteUpload;

public class CompleteUploadHandler : IRequestHandler<CompleteUploadCommand, CompleteUploadResponse>
{
    private readonly IContentRepository _contentRepository;

    public CompleteUploadHandler(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public async Task<CompleteUploadResponse> Handle(CompleteUploadCommand request, CancellationToken cancellationToken)
    {
        var content = await _contentRepository.GetByIdAsync(request.ContentId);
        
        if (content == null)
        {
            throw new KeyNotFoundException($"Content with ID {request.ContentId} not found");
        }

        content.Status = "Processing";
        content.BlobUrl = request.BlobUrl;
        content.UploadedAt = DateTime.UtcNow;

        await _contentRepository.UpdateAsync(content);

        return new CompleteUploadResponse
        {
            Success = true
        };
    }
}